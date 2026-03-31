# SolidEdge Engineering Add-in

A **Solid Edge COM Add-in** built in VB.NET that automates two engineering workflows: extracting and versioning Bill of Materials (BOM) data into a SQL Server database, exporting drawing sheets as PDF and DXF files and exporting 3D documents as STEP and IGS files.

This project is used in a real manufacturing environment by a mechanical engineering team.

---

## Features

### BOM Extractor
- Recursively traverses the full assembly tree including sub-assemblies
- Reads part properties directly from Solid Edge: item code, title, material, category, status, author, and specifications
- Detects and resolves the associated drawing (`.dft`) and PDF files for each component
- Compares the current BOM against the previous version — only saves if changes are detected
- Stores versioned BOM snapshots in SQL Server with full parent-child relationships

### PDF and DXF Exporter
- Exports the `Desenho` sheet of the active drawing as a PDF
- Exports the `PO`, `LA`, and `POLA` sheets as DXF files — only sheets that contain drawing views
- Automatically deletes outdated `.dwg` files before saving new ones
- All files are saved to a configured network output path

### STEP and IGS Exporter
- Exports the active Part or SheetMetal document as STEP or IGS
- All files are saved to a configured network output path
- Deletes the auto-generated LOG file when exporting as STEP 

---

## Tech Stack

- **Language:** VB.NET — .NET Framework 4.8
- **CAD Integration:** Solid Edge COM Interop API (`SolidEdgeAssembly`, `SolidEdgeFramework`, `SolidEdgeFileProperties`)
- **Database:** Microsoft SQL Server via ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`)
- **Platform:** Windows desktop — runs as a Solid Edge Add-in

---

## Project Structure

```
SolidEdgeAddIn/
│
├── Commands/
│   ├── ExtractBOM.vb          # Orchestrates the BOM extraction process
│   ├── ExportDft.vb           # Handles PDF and DXF export
│   └── Export3D.vb            # Handles STEP and IGS export
│
├── Models/
│   └── BomItem.vb             # Data model representing a single BOM component
│
├── Repositories/
│   └── BomRepository.vb       # All database operations (read and write)
│
├── Services/
│   └── SolidEdgePropertyReader.vb  # Reads properties from Solid Edge documents
│
├── AppSettings.vb             # Loads configuration from app.secrets.config
├── Connect.vb                 # Add-in entry point — registers commands and buttons
├── app.config                 # Assembly binding redirects
└── app.secrets.config         # Sensitive configuration (not committed — see configuration)
```

---

## Configuration

All sensitive values are stored in `app.secrets.config`, which is excluded from version control via `.gitignore`. You must create this file manually before building.

Create `app.secrets.config` in the project root with the following structure:

```xml
<?xml version="1.0" encoding="utf-8" ?>
<appSettings>
    <add key="ConnectionString"   value="Server=YOUR_SERVER;Database=YOUR_DB;Trusted_Connection=True;"/>
    <add key="PdfDxfOutputPath"   value="\\your-server\shared-folder\"/>
    <add key="StepIgsOutputPath"  value="\\your-server\shared-folder\"/>
    <add key="IconBomPath"        value="\\your-server\path\to\iconBOM32.bmp"/>
    <add key="IconDftPath"        value="\\your-server\path\to\export_pdf_dxf_icon.bmp"/>
    <add key="IconStepPath"        value="\\your-server\path\to\export_Step_icon.bmp"/>
    <add key="IconIgsPath"        value="\\your-server\path\to\export_Igs_icon.bmp"/>
</appSettings>
```

After creating the file, set its properties in Visual Studio:
- **Build Action** → `None`
- **Copy to Output Directory** → `Copy if newer`

---

## Database

The add-in writes to two SQL Server tables:

**`BomVersion`** — one record per BOM extraction
| Column | Description |
|---|---|
| `Id` | Primary key |
| `Serial` | Assembly serial number |
| `Version` | Version number (auto-incremented) |
| `ExtractionDate` | Timestamp |
| `Issuer` | Username of the issuer |
| `Title` | Assembly title |
| `Customer` | Customer name |

**`BomItem`** — one record per component per version
| Column | Description |
|---|---|
| `Id` | Primary key |
| `BomVersionId` | Foreign key to `BomVersion` |
| `ParentItemId` | Self-referencing foreign key for tree structure |
| `Category` | Category (FABRICADO, COMPRADO, ESTOQUE, PADRÃO) |
| `ItemCode` | Part item code |
| `Quantity` | Quantity in the assembly |
| `Title` | Part title |
| `Specifications` | Part details |
| `Material` | Material |
| `StatusSE` | Solid Edge lifecycle status |
| `PathPDF` | Path to the PDF file |
| `Path3D` | Path to the 3D model file |
| `PathParent` | Path to the 3D model of the parent |
| `PathDFT` | Path to the drawing file |
| `LastAutor3D` | Last athor of the 3D model |
| `CreatorDFT` | Author of the drawing |
| `Creator3D` | Author of the 3D model |
| `Revision` | Revision number of the 3D model |