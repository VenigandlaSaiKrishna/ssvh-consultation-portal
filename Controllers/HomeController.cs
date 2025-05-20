using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SSVH_Consultation_Poratl.ActionFilters;
using SSVH_Consultation_Poratl.Models;
using System.Diagnostics;


namespace SSVH_Consultation_Poratl.Controllers
{
    [RoleFilter(1, 2)]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var data = await _context.Consultation.ToListAsync();
            return View(data);
        }

        public async Task<IActionResult> Create()
        {
            var clssData = await _context.ClassNames.ToListAsync();
            var classData = await _context.Classes.ToListAsync();
            var stationaryData = await _context.Stationary.ToListAsync();
            var transportData = await _context.Transport.ToListAsync();
            ViewBag.ClassList = classData;

            List<CustomUniformModel> lstCustomUniformModel = new List<CustomUniformModel>();
            foreach (var item in stationaryData)
            {
                CustomUniformModel customUniformModel = new CustomUniformModel();
                customUniformModel.Id = item.Id;
                customUniformModel.UniformInfo = item.Gender.ToLowerInvariant() == "male" ?
                    item.Gender + " (Shirt Size : " + item.ShirtSize + " -- Pant Size : " + item.PantSize + ")" :
                    item.Gender + "(Shirt Size : " + item.ShirtSize + " -- Skirt Size : " + item.SkirtSize + ")";
                customUniformModel.Amount = item.Amount;
                customUniformModel.ClassName = item.ClassName;
                customUniformModel.BeltTieSocksAmount = item.BeltTieSocksAmount;
                lstCustomUniformModel.Add(customUniformModel);
            }

            ViewBag.ClaaNames = new SelectList(clssData, "Id", "ClassName");
            ViewBag.StationaryUniformSizeData = new SelectList(lstCustomUniformModel, "Id", "UniformInfo");
            ViewBag.StationaryUniformAmountData = new SelectList(stationaryData, "Id", "Amount");
            ViewBag.StationaryBeltTieSocksAmountData = new SelectList(stationaryData, "Id", "BeltTieSocksAmount");
            ViewBag.TransportData = new SelectList(transportData, "Id", "AreaName");
            ViewBag.TransportList = transportData;
            ViewBag.UniformList = lstCustomUniformModel;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateConsultationViewModel model)
        {
            if (ModelState.IsValid)
            {
                Consultation consultation = new Consultation();
                consultation.Status = model.Status;
                consultation.StudentName = model.StudentName;
                consultation.ParentName = model.ParentName;
                consultation.AcadamicFees   = model.AcadamicFees;
                consultation.ClassName = model.ClassName;
                consultation.TransportAmount = model.TransportAmount;
                consultation.AcadamicYear = model.AcadamicYear;
                consultation.AdmissionFees = model.AdmissionFees;
                consultation.IsAcadamicFeesDiscount = model.IsAcadamicFeesDiscount;
                consultation.AcadamicFeesDiscount = model.AcadamicFeesDiscount;
                consultation.BooksAmount = model.BooksAmount;
                consultation.CreatedBy = HttpContext.Session.GetString("UserName") ?? "";
                consultation.CreatedOn = DateTime.Now;
                consultation.Mobile = model.Mobile;
                consultation.UniformAmount = model.UniformAmount;
                consultation.BeltTieSocksAmount = model.BeltTieSocksAmount;
                consultation.TransportId = model.TransportId;
                consultation.UniformData =  model.UniformData;
                consultation.PreviousSchoolName = model.PreviousSchoolName;

                _context.Consultation.Add(consultation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var clssData = await _context.ClassNames.ToListAsync();
            var classData = await _context.Classes.ToListAsync();
            var stationaryData = await _context.Stationary.ToListAsync();
            var transportData = await _context.Transport.ToListAsync();
            ViewBag.ClassList = classData;

            List<CustomUniformModel> lstCustomUniformModel = new List<CustomUniformModel>();
            foreach (var item in stationaryData)
            {
                CustomUniformModel customUniformModel = new CustomUniformModel();
                customUniformModel.Id = item.Id;
                customUniformModel.UniformInfo = item.Gender.ToLowerInvariant() == "male" ?
                    item.Gender + " (Shirt Size : " + item.ShirtSize + " -- Pant Size : " + item.PantSize + ")" :
                    item.Gender + "(Shirt Size : " + item.ShirtSize + " -- Skirt Size : " + item.SkirtSize + ")";
                customUniformModel.Amount = item.Amount;
                customUniformModel.ClassName = item.ClassName;
                customUniformModel.BeltTieSocksAmount = item.BeltTieSocksAmount;
                lstCustomUniformModel.Add(customUniformModel);
            }

            ViewBag.ClaaNames = new SelectList(clssData, "Id", "ClassName");
            ViewBag.StationaryUniformSizeData = new SelectList(lstCustomUniformModel, "Id", "UniformInfo");
            ViewBag.StationaryUniformAmountData = new SelectList(stationaryData, "Id", "Amount");
            ViewBag.StationaryBeltTieSocksAmountData = new SelectList(stationaryData, "Id", "BeltTieSocksAmount");
            ViewBag.TransportData = new SelectList(transportData, "Id", "AreaName");
            ViewBag.TransportList = transportData;
            ViewBag.UniformList = lstCustomUniformModel;

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var consultationEntity = await _context.Consultation.FindAsync(id);
            if (consultationEntity == null)
            {
                return NotFound();
            }

            CreateConsultationViewModel consultationVMEntity = new CreateConsultationViewModel();
            consultationVMEntity.Status=consultationEntity.Status;
            consultationVMEntity.StudentName=consultationEntity.StudentName;
            consultationVMEntity.ParentName=consultationEntity.ParentName;
            consultationVMEntity.AcadamicFees=consultationEntity.AcadamicFees;
            consultationVMEntity.ClassName=consultationEntity.ClassName;
            consultationVMEntity.TransportAmount=consultationEntity.TransportAmount;
            consultationVMEntity.AcadamicYear=consultationEntity.AcadamicYear;
            consultationVMEntity.AdmissionFees=consultationEntity.AdmissionFees;
            consultationVMEntity.IsAcadamicFeesDiscount=consultationEntity.IsAcadamicFeesDiscount;
            consultationVMEntity.AcadamicFeesDiscount=consultationEntity.AcadamicFeesDiscount;
            consultationVMEntity.BooksAmount=consultationEntity.BooksAmount;
            consultationVMEntity.Mobile=consultationEntity.Mobile;
            consultationVMEntity.UniformAmount=consultationEntity.UniformAmount;
            consultationVMEntity.BeltTieSocksAmount= consultationEntity.BeltTieSocksAmount;
            consultationVMEntity.TransportId= consultationEntity.TransportId;
            consultationVMEntity.UniformData =  consultationEntity.UniformData;
            consultationVMEntity.CreatedBy =  consultationEntity.CreatedBy;
            consultationVMEntity.CreatedOn =  consultationEntity.CreatedOn;
            consultationVMEntity.PreviousSchoolName = consultationEntity.PreviousSchoolName;

            var clssData = await _context.ClassNames.ToListAsync();
            var classData = await _context.Classes.ToListAsync();
            var stationaryData = await _context.Stationary.ToListAsync();
            var transportData = await _context.Transport.ToListAsync();
            ViewBag.ClassList = classData;

            List<CustomUniformModel> lstCustomUniformModel = new List<CustomUniformModel>();
            foreach (var item in stationaryData)
            {
                CustomUniformModel customUniformModel = new CustomUniformModel();
                customUniformModel.Id = item.Id;
                customUniformModel.UniformInfo = item.Gender.ToLowerInvariant() == "male" ?
                    item.Gender + " (Shirt Size : " + item.ShirtSize + " -- Pant Size : " + item.PantSize + ")" :
                    item.Gender + "(Shirt Size : " + item.ShirtSize + " -- Skirt Size : " + item.SkirtSize + ")";
                customUniformModel.Amount = item.Amount;
                customUniformModel.ClassName = item.ClassName;
                customUniformModel.BeltTieSocksAmount = item.BeltTieSocksAmount;
                lstCustomUniformModel.Add(customUniformModel);
            }

            ViewBag.ClaaNames = new SelectList(clssData, "Id", "ClassName");
            ViewBag.StationaryUniformSizeData = new SelectList(lstCustomUniformModel, "Id", "UniformInfo");
            ViewBag.StationaryUniformAmountData = new SelectList(stationaryData, "Id", "Amount");
            ViewBag.StationaryBeltTieSocksAmountData = new SelectList(stationaryData, "Id", "BeltTieSocksAmount");
            ViewBag.TransportData = new SelectList(transportData, "Id", "AreaName");
            ViewBag.TransportList = transportData;
            ViewBag.UniformList = lstCustomUniformModel;



            return View(consultationVMEntity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Consultation model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                model.UpdatedBy = HttpContext.Session.GetString("UserName") ?? "";
                model.UpdatedOn = DateTime.Now;
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public JsonResult GetClassesByAY(string acadamicYear)
        {
            var clssData = _context.Classes.Where(x => x.AcadamicYear == acadamicYear).ToList();
            return Json(clssData);
        }

        [HttpPost]
        public async Task<IActionResult> Print(int id)
        {
            var data = await _context.Consultation.FindAsync(id);
            if (data == null)
                return NotFound("Consultation data not found.");
            var pdfPrinter = new PDFPrinter();
            byte[] pdfBytes = pdfPrinter.GenerateConsultationReport(data);
            return File(pdfBytes, "application/pdf", "Consultation_Report.pdf");
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    Document doc = new Document();
            //    PdfWriter.GetInstance(doc, ms);
            //    doc.Open();
            //    doc.Add(new Paragraph("Consultation Report"));
            //    doc.Add(new Paragraph($"Student: {data.StudentName}"));
            //    doc.Add(new Paragraph($"Parent: {data.ParentName}"));
            //    doc.Add(new Paragraph($"Mobile: {data.Mobile}"));
            //    doc.Add(new Paragraph($"Academic Year: {data.AcadamicYear}"));
            //    doc.Add(new Paragraph($"Class: {data.ClassName}"));
            //    doc.Add(new Paragraph($"Transport Amount: {data.TransportAmount}"));
            //    doc.Add(new Paragraph($"Academic Fees: {data.AcadamicFees}"));
            //    doc.Close();

            //    byte[] pdfBytes = ms.ToArray();

            //    return File(pdfBytes, "application/pdf", "Consultation_Report.pdf");
            //}
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
