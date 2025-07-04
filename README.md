<img src="https://github.com/user-attachments/assets/ab136cae-62df-4c13-94b1-0e7d32721729" width="75"> <span style="margin-left: 2px;">**Support Bindr**</span>

**Bindr** is a Windows Forms utility app built to automate and streamline document workflows related to JDE Riders, BOMs, NestPlans, and more — tailored for use in an industrial or manufacturing environment.

---

## 🚀 Key Features

### 📊 JDE Reporting (In Development)

**JDE Reporting** is designed to enhance support tracking and reporting for your industrial and manufacturing processes. Currently, it supports loading reports from an Excel file, with plans to integrate SQL data sources for future versions. Key features include:

- 📂 **Load Reports**: Import and view a summarized roll-up of reports, showing a list of jobs and the quantity of supports per status.
- 🔍 **Job & Status Drill-Down**: Double-click on a job or status to see a detailed view of all supports for that specific job and status.
- ⚙️ **Right-Click Functionality**:
  - View support details by right-clicking any cell in the report.
  - Open merged PO files for further inspection directly from any cell.
- 📊 **FG Code Breakdown**: View and total supports by FG code for a more detailed analysis.
- 🔄 **Future Features** (Planned):
  - Revenue roll-ups, including daily, weekly, monthly, and yearly breakdowns.
  - Track and visualize changes over time, with easy-to-read graphs and charts.
  - Additional reporting enhancements for more granular control over support data.
---

The future updates will provide powerful insights into support statuses and allow for more sophisticated tracking, helping you make data-driven decisions in real time.

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
- **[AdvancedDataGridView](https://www.nuget.org/packages/AdvancedDataGridView/)** – For advanced grid features like filtering, sorting, and column visibility.
- **[iTextSharp.LGPLv2.Core](https://www.nuget.org/packages/iTextSharp.LGPLv2.Core/)** – For parsing, manipulating, and extracting raw text from PDFs.
- **[DocumentFormat.OpenXml](https://www.nuget.org/packages/DocumentFormat.OpenXml/)** – For reading and extracting data from modern `.xlsx` spreadsheets (used in Load SO / Load BOM).
- **[CsvHelper](https://www.nuget.org/packages/CsvHelper/)** – For parsing and cleaning CSV files like BOMs with speed and reliability.
- **[System.Text.Encoding.CodePages](https://www.nuget.org/packages/System.Text.Encoding.CodePages/)** – Ensures correct encoding interpretation in some file formats.
- **[Dapper](https://www.nuget.org/packages/Dapper/)** – *(Planned)* Lightweight SQL ORM to power fast, clean queries once database integration is added.
- **[LiveChartsCore.SkiaSharpView.WinForms](https://www.nuget.org/packages/LiveChartsCore.SkiaSharpView.WinForms/)** *(Planned)* – A modern charting library for live, interactive dashboards and reports.

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



### 📚 Moden Reporting Dashboards

  Integration with **LiveCharts2** to show clean, real-time visualizations:
  
  - Support status overview
  - Release readiness
  - Financial tracking
  - NestPlan or fabrication throughput

---

## 📂 Installation

Clone this repo and open the project in Visual Studio:

```bash
git clone https://github.com/JaredCH/Bindr.git
