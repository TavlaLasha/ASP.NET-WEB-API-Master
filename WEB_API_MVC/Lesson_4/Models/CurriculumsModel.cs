using Lesson_4.EF;
using Lesson_4.Reusables;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Lesson_4.Models
{
    public class CurriculumsModel
    {
        #region Methods
        public CurriculumsResponse GetCurriculums()
        {
            var Result = new CurriculumsResponse();

            using (var db = new LearningCenterEntities())
            {
                Result.Curriculums = db.Curricula.Select(i => new CurriculumDTO
                {
                    ID = i.ID,
                    SubjectID = i.SubjectID,
                    LecturerID = i.LecturerID,
                    SubjectName = i.Subject.Name,
                    LecturerName = i.Lecturer.FirstName + " " + i.Lecturer.LastName,
                    Syllabus = i.Syllabus

                }).ToList();
                Result.IsSuccess = true;
            }

            return Result;
        }

        public ApiResponseBase AddCurriculum(CurriculumDTO SubmitModel)
        {
            var Result = new ApiResponseBase();
            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    Curriculum Curriculum = new Curriculum
                    {
                        SubjectID = SubmitModel.SubjectID,
                        LecturerID = SubmitModel.LecturerID,
                        Syllabus = SubmitModel.Syllabus
                    };
                    db.Curricula.Add(Curriculum);
                    db.SaveChanges();
                    Result.IsSuccess = true;
                }
            }
            else
            {
                Result.IsSuccess = false;
                Result.ErrorMessage = Constants.ApiResponseMessages.NoDataGiven;
                Result.StatusCode = HttpStatusCode.BadRequest;
            }
            return Result;
        }

        public ApiResponseBase UpdateCurriculum(CurriculumDTO SubmitModel)
        {
            var Result = new ApiResponseBase();

            if (SubmitModel != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (SubmitModel.ID != null && !db.Curricula.Any(i => i.ID == SubmitModel.ID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var CurriculumToUpdate = db.Curricula.Where(i => i.ID == SubmitModel.ID).First();

                        CurriculumToUpdate.SubjectID = SubmitModel.SubjectID;
                        CurriculumToUpdate.LecturerID = SubmitModel.LecturerID;
                        CurriculumToUpdate.Syllabus = SubmitModel.Syllabus;
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }

        public ApiResponseBase DeleteCurriculum(int? CurriculumID)
        {
            var Result = new ApiResponseBase();

            if (CurriculumID != null)
            {
                using (var db = new LearningCenterEntities())
                {
                    if (!db.Curricula.Any(i => i.ID == CurriculumID))
                    {
                        Result.ErrorMessage = Constants.ApiResponseMessages.NotFound;
                        Result.StatusCode = HttpStatusCode.NotFound;
                    }
                    else
                    {
                        var r = db.Curricula.Where(i => i.ID == CurriculumID).First();
                        db.Curricula.Remove(r);
                        db.SaveChanges();
                        Result.IsSuccess = true;
                    }
                }
            }

            return Result;
        }
        #endregion

        #region Sub Classes
        public class CurriculumDTO
        {
            public int? ID { get; set; }
            public int SubjectID { get; set; }
            public int LecturerID { get; set; }
            public string SubjectName { get; set; }
            public string LecturerName { get; set; }
            public byte[] Syllabus { get; set; }
        }

        public class CurriculumsResponse : ApiResponseBase
        {
            public List<CurriculumDTO> Curriculums { get; set; }
        }
        #endregion
    }
}