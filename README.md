# 📎 Bindr

**Bindr** is a Windows Forms utility app built to automate and streamline document workflows related to JDE Riders, BOMs, NestPlans, and more — tailored for use in an industrial or manufacturing environment.

---

## 🚀 Key Features

### 🧠 Smart PDF Extraction + Merging
- Load in a PDF (e.g., a JDE Rider).
- Automatically extract specific text from each page.
- Match and merge support detail PDFs with the Rider PDF using the extracted data.
- Time Saved: 5-30 minutes per PO.
---

### 📄 Process BOM
- Open a BOM CSV file.
- Automatically:
  - Replace blank fields with `0`.
  - Strip out odd characters like `%`.
  - Replace `#` symbols with `0`.
- Cleaned data is copied to the clipboard and is ready to paste into the PS tool.
- Time Saved: 1-2 minutes per PO.
---

### 📥 Load SO / Load BOM
- Reads `.xlsv` files in the background (non-blocking).
- Extracts relevant SO or BOM data and places it on the clipboard.
- Designed for quick transfer into JDE with no manual cleanup.
- Time Saved: 30-45 seconds per PO.
---

### 🗺️ PDF Coordinate Mapping (NEW!)
- Define up to **4 mappable coordinates** on a 'smart' PDF (text-based, not scanned images).
- Bindr processes every page using the mapped areas to extract relevant data across the document.

---

### 🧾 NestPlan Data Viewer (NEW!)
- Parses and displays metadata from NestPlan files (plasma metal cutter machine instructions).
- Extracts part numbers and key attributes.
- Includes a **right-click preview** option to show NestPlan PDF or related support detail (initial functionality implemented).
- Future support planned: live data access via SQL integration.

---

## ⚙️ Under the Hood

Bindr is built in **C# WinForms** and powered by a blend of libraries that enable robust document workflows, smart automation, and smooth UI performance.

### 🧰 Key NuGet Packages

- **[PdfiumViewer](https://www.nuget.org/packages/PdfiumViewer/)** – For rendering and navigating PDF pages inside the app.
- **[iTextSharp.LGPLv2.Core](https://www.nuget.org/packages/iTextSharp.LGPLv2.Core/)** – For parsing, manipulating, and extracting raw text from PDFs.
- **[DocumentFormat.OpenXml](https://www.nuget.org/packages/DocumentFormat.OpenXml/)** – For reading and extracting data from modern `.xlsx` spreadsheets (used in Load SO / Load BOM).
- **[CsvHelper](https://www.nuget.org/packages/CsvHelper/)** – For parsing and cleaning CSV files like BOMs with speed and reliability.
- **[System.Text.Encoding.CodePages](https://www.nuget.org/packages/System.Text.Encoding.CodePages/)** – Ensures correct encoding interpretation in some file formats.
- **[Dapper](https://www.nuget.org/packages/Dapper/)** – *(Planned)* Lightweight SQL ORM to power fast, clean queries once database integration is added.

### 🧪 Core Features Powered By

- PDF coordinate mapping using Pdfium + iTextSharp for multi-page text harvesting from smart PDFs.
- Background threading for fast, non-blocking file loading (Load SO / Load BOM / Nestplan parsing).
- Smart clipboard automation for copying processed data into external tools like JDE / PS.


---

## 🛠️ Planned Features

### 🔌 SQL Integration (In Development)

A major leap forward: integrating Bindr with your existing SQL database will unlock powerful real-time workflows. Once connected, Bindr will support:

- 📂 Viewing **live support statuses** directly from the database.
- 🔍 Instant access to **support BOMs** for verification, traceability, and planning.
- 📝 **Auto-generated reports** to manage support production flow.
- 🚪 Enables transition into a **paperless workflow** — no more manual BOM sorting or status tracking.
- ⚙️ Smart grouping and sorting of supports:
  - By **plate material** and **thickness**
  - By **pipe size**, **beam size**, or **angle size** (for non-plate supports)
  - Resulting in a **"digital cubby"** system for managing all in-progress support work.

---

### 📦 Release Evaluation System

With live BOMs and on-hand stock pulled from SQL, Bindr will be able to:

- Instantly determine **what can be released** based on current inventory.
- Generate **release-ready reports** with options to:
  - Release & print
  - Batch email downstream teams
  - Update internal statuses

---

### 🖥️ Full-Size PDF Viewing

- Users will be able to **right-click any support** or **nestplan record** and open the full PDF in-app — no external viewer required.
- Smooth, embedded experience for checking details without losing workflow context.

---

### 📚 Batch PDF Merge Workflow

A huge boost for efficiency:

- Combine multiple support details or nestplans into a single PDF **package**.
- Each batch can be:
  - Drawn
  - Released
  - Status-updated
  - Printed
  - Emailed
- Seamlessly integrated into the **digital cubby** system, empowering teams to process grouped supports faster and smarter.


---

## 📂 Installation

Clone this repo and open the project in Visual Studio:

```bash
git clone https://github.com/JaredCH/Bindr.git
