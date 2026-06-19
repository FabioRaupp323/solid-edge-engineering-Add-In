# SolidEdge Engineering Add-in

A **Solid Edge COM Add-in** built in VB.NET that automates engineering workflows: extracting and versioning Bill of Materials (BOM) data into SQL Server, exporting drawing sheets as PDF and DXF files, exporting 3D documents as STEP and IGS files, and registering or updating ERP products directly from the CAD context.

This project is used in a real manufacturing environment by a mechanical engineering team.

---

## Features

### BOM Extractor
- Recursively traverses the full assembly tree, including sub-assemblies
- Reads part properties directly from Solid Edge: item code, title, material, category, status, author, and specifications
- Detects the associated drawing (`.dft`) and exported PDF for each component
- Compares the current BOM against the previous version and only saves when changes are detected
- Stores versioned BOM snapshots in SQL Server with full parent-child relationships

### PDF and DXF Exporter
- Exports the `Desenho` sheet of the active drawing as PDF
- Exports the `PO`, `LA`, and `POLA` sheets as DXF files when they contain drawing views
- Deletes outdated `.dwg` files before saving new exports
- Saves generated files to a configured network output path

### STEP and IGS Exporter
- Exports the active Part or SheetMetal document as STEP or IGS
- Saves generated files to a configured network output path
- Deletes the auto-generated LOG file when exporting STEP

### ERP Product Registration
- Available in the Assembly, Part, and SheetMetal environments
- Opens a registration form pre-filled from the active Solid Edge document properties
- Reads the current product from `SummaryInformation`:
  - `Keywords` -> item code
  - `Title` -> description
  - `Comments` -> reference
- Lets the user search ERP products by code, description, or reference
- Uses debounced, cancellable async search against the ERP database
- Validates required description and reference fields before saving
- If the active document has no ERP item code, duplicates a selected base product and assigns a new product code
- If the active document already has an ERP item code, verifies that the product exists and updates its description and reference
- Writes the final ERP values back to the active Solid Edge document properties:
  - item code -> `Keywords`
  - description -> `Title`
  - reference -> `Comments`
- Generates new product codes inside a SQL transaction with row-level locking, preventing duplicate codes between concurrent registrations made through this add-in

---

## Tech Stack

- **Language:** VB.NET, .NET Framework 4.8
- **CAD Integration:** Solid Edge COM Interop API
- **UI:** Windows Forms
- **Database:** Microsoft SQL Server via ADO.NET
- **Platform:** Windows desktop, running as a Solid Edge COM Add-in
- **Interop:** Solid Edge Assembly, Draft, Part, Framework, FrameworkSupport, FileProperties, Geometry, and Constants COM references

---

## Project Structure

```text
SolidEdgeAddIn/
│
├── AddInRegistry/
│   ├── Connect.vb              # Add-in entry point; connects to Solid Edge, registers environments, and dispatches commands
│   ├── CommandRegistry.vb      # Registers command groups, buttons, captions, IDs, and icons
│   └── SolidEdgeCATIDs.vb      # Named constants for Solid Edge environment GUIDs
│
├── Commands/
│   ├── ExtractBOM.vb           # Orchestrates BOM extraction
│   ├── ExportDft.vb            # Handles PDF and DXF export
│   ├── Export3D.vb             # Handles STEP and IGS export
│   └── RegisterProduct.vb      # Builds the current product, opens the form, persists ERP changes, and syncs document properties
│
├── Forms/
│   └── RegisterProductForm.vb  # ERP product registration UI with async base-product search and validation
│
├── Models/
│   ├── BomItem.vb              # Data model representing a BOM component
│   └── ErpProduct.vb           # Data model representing an ERP product
│
├── Repositories/
│   ├── BomRepository.vb        # BOM database read/write operations
│   └── ErpRepository.vb        # ERP search, existence check, update, and product duplication operations
│
├── Services/
│   └── SolidEdgePropertyReader.vb  # Reads and writes Solid Edge document properties
│
├── AppSettings.vb              # Loads required settings from app.secrets.config
├── app.config                  # Assembly binding redirects
├── app.secrets.config          # Sensitive local configuration, copied to output
└── SolidEdgeAddIn.vbproj       # VB.NET project file
```

---

## Configuration

Sensitive values are stored in `app.secrets.config`.

The application loads this file from the same folder as the compiled add-in DLL. During development, keep `app.secrets.config` in the project root and configure it to copy to the output directory.

Create `app.secrets.config` with this structure:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<appSettings>
    <add key="BomConnectionString"       value="Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"/>
    <add key="ErpConnectionString"       value="Server=ERP_SERVER;Database=ERP_DB;User Id=ERP_USER;Password=ERP_PASSWORD;"/>
    <add key="PdfDxfServerPath"          value="\\your-server\shared-folder\"/>
    <add key="StepIgsServerPath"         value="\\your-server\shared-folder\"/>
    <add key="IconBomPath"               value="\\your-server\path\to\iconBOM32.bmp"/>
    <add key="IconDftPath"               value="\\your-server\path\to\export_pdf_dxf_icon.bmp"/>
    <add key="IconStepPath"              value="\\your-server\path\to\export_Step_icon.bmp"/>
    <add key="IconIgsPath"               value="\\your-server\path\to\export_Igs_icon.bmp"/>
    <add key="IconCadastrarProdutoPath"  value="\\your-server\path\to\cadastrar_produto_icon.bmp"/>
    <add key="ErpDuplicatedColumns"      value="Column1,Column2,Column3"/>
</appSettings>
```

In Visual Studio, set `app.secrets.config` to:

- **Build Action:** `None`
- **Copy to Output Directory:** `Copy if newer`

`ErpDuplicatedColumns` is a comma-separated list of ERP `Produto` table columns copied verbatim when duplicating a base product. The item code, description, and reference are handled explicitly by the add-in and should not be included in this list.

---

## Solid Edge Commands

The add-in registers command buttons according to the active Solid Edge environment.

| Environment | Command group | Commands |
|---|---|---|
| Assembly | `Análise de BOM` | `ExportarBOM` |
| Assembly | `Integração CAD x ERP` | `Cadastrar Produto` |
| Draft | `Exportar DFT` | `Exportar PDF e DXF` |
| Part | `Exportar 3D` | `Exportar STEP`, `Exportar IGS` |
| Part | `Integração CAD x ERP` | `Cadastrar Produto` |
| SheetMetal | `Exportar 3D` | `Exportar STEP`, `Exportar IGS` |
| SheetMetal | `Integração CAD x ERP` | `Cadastrar Produto` |

---

## Database

The add-in reads from and writes to two separate SQL Server databases.

### BOM Database

**`BomVersion`** stores one record per BOM extraction.

| Column | Description |
|---|---|
| `Id` | Primary key |
| `Serial` | Assembly serial number |
| `Version` | Version number |
| `ExtractionDate` | Extraction timestamp |
| `Issuer` | Username of the issuer |
| `Title` | Assembly title |
| `Customer` | Customer name |

**`BomItem`** stores one record per component per BOM version.

| Column | Description |
|---|---|
| `Id` | Primary key |
| `BomVersionId` | Foreign key to `BomVersion` |
| `ParentItemId` | Self-referencing foreign key for the BOM tree |
| `Category` | Component category |
| `ItemCode` | Part item code |
| `Quantity` | Quantity in the assembly |
| `Title` | Part title |
| `Specifications` | Part details |
| `Material` | Material |
| `StatusSE` | Solid Edge lifecycle status |
| `PathPDF` | Path to the exported PDF |
| `Path3D` | Path to the 3D model |
| `PathParent` | Path to the parent 3D model |
| `PathDFT` | Path to the drawing file |
| `LastAutor3D` | Last author of the 3D model |
| `CreatorDFT` | Author of the drawing |
| `Creator3D` | Author of the 3D model |
| `Revision` | Revision number of the 3D model |

### ERP Database

The add-in connects to the company's existing ERP database. It does not own or control that schema.

Only two ERP tables are used:

- **`Produto`**: product registry table. The add-in searches it by code, description, and reference, checks whether a product exists, updates description/reference fields, and creates new products by duplicating an existing product record under a new code.
- **`Config`**: key/value configuration table. The add-in reads and increments the last-issued product code stored under the `Produto` key.

The full `Produto` column list is intentionally not documented because it belongs to a private third-party ERP schema. Columns that must be copied during duplication are configured through `ErpDuplicatedColumns`.

---

## ERP Registration Flow

1. The command reads the active Solid Edge document properties into an `ErpProduct`.
2. The form opens with item code, description, and reference pre-filled.
3. The user edits description/reference and, when creating a new product, selects a base ERP product.
4. If the item code is empty, the add-in duplicates the selected base product and assigns the next ERP code.
5. If the item code is already filled, the add-in verifies the product exists and updates its description/reference.
6. The add-in writes the final item code, description, and reference back to the active Solid Edge document.