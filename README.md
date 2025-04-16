# 📎 Bindr

**Bindr** is a Windows Forms utility app built to automate and streamline document workflows related to JDE Riders, BOMs, NestPlans, and more — tailored for use in an industrial or manufacturing environment.

---

## 🚀 Key Features

### 🧠 Smart PDF Extraction + Merging
- Load in a PDF (e.g., a JDE Rider).
- Automatically extract specific text from each page.
- Match and merge support detail PDFs with the Rider PDF using the extracted data.

---

### 📄 Process BOM
- Open a BOM CSV file.
- Automatically:
  - Replace blank fields with `0`.
  - Strip out odd characters like `%`.
  - Replace `#` symbols with `0`.
- Cleaned data is copied to the clipboard, ready to paste into the PS tool.

---

### 📥 Load SO / Load BOM
- Reads `.xlsv` files in the background (non-blocking).
- Extracts relevant SO or BOM data and places it on the clipboard.
- Designed for quick transfer into JDE with no manual cleanup.

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

- Built in **C# WinForms**.
- Uses **Pdfium** for PDF rendering and text extraction.
- Optimized for clipboard workflows, so you can move clean, parsed data directly into downstream tools like JDE and the PS tool with a single paste.

---

🛠️ Planned Features
🔌 SQL Integration (In Development)
A major leap forward: integrating Bindr with your existing SQL database will unlock powerful real-time workflows. Once connected, Bindr will support:

📂 Viewing live support statuses directly from the database.

🔍 Instant access to support BOMs for verification, traceability, and planning.

📝 Auto-generated reports to manage support production flow.

🚪 Enables transition into a paperless workflow — no more manual BOM sorting or status tracking.

⚙️ Smart grouping and sorting of supports:

By plate material and thickness

By pipe size, beam size, or angle size (for non-plate supports)

Resulting in a "digital cubby" system for managing all in-progress support work.

📦 Release Evaluation System
With live BOMs and on-hand stock pulled from SQL, Bindr will be able to:

Instantly determine what can be released based on current inventory.

Generate release-ready reports with options to:

Release & print

Batch email downstream teams

Update internal statuses

🖥️ Full-Size PDF Viewing
Users will be able to right-click any support or nestplan record and open the full PDF in-app — no external viewer required.

Smooth, embedded experience for checking details without losing workflow context.

📚 Batch PDF Merge Workflow
A huge boost for efficiency:

Combine multiple support details or nestplans into a single PDF package.

Each batch can be:

Drawn

Released

Status-updated

Printed

Emailed

Seamlessly integrated into the digital cubby system, empowering teams to process grouped supports faster and smarter.

---

## 📂 Installation

Clone this repo and open the project in Visual Studio:

```bash
git clone https://github.com/JaredCH/Bindr.git
