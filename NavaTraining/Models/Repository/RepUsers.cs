using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NavaTraining.Models;

namespace NavaTraining.Models.Repository
{
    public class RepUsers
    {

        Nava_DBEntities db = new Nava_DBEntities();

        public List<VM_DetailsUsers> DetailsUsers(int UserID)
        {
            var q = (from a in db.RegisterUser
                     join b in db.EducatInfo on a.UserID equals b.UserID
                     join c in db.Fields on b.FieldID equals c.FieldID       
                     join e in db.UniverCity on b.UniID equals e.UniID
                     join f in db.CrossSection on b.SectionID equals f.SectionID
                     join j in db.Orientation on b.OrientationID equals j.OrientationID
                     where a.UserID == UserID
                     select new VM_DetailsUsers
                     {
                         SectionName=f.SectionTitle,
                         UniName=e.UniName,
                         FieldName=c.FieldName,
                         OrientationName=j.OrientationName,
                         ThesisDiss=b.ThesisDiss,
                         BeginStudy=b.BeginStudy,
                         Avrage=b.Avrage,
                         YearGrad=b.YearGrad,
                         Description=b.Description,
                         Stu_Graduate=b.Stu_Graduate
                     }).Distinct().OrderBy(a => a.BeginStudy);
            return q.ToList();
        }

        public List<VM_UserSkills> UserSkills(int UserID)
        {
            var q = (from a in db.RegisterUser
                     join b in db.Field_Lessons on a.UserID equals b.UserID
                     join c in db.Lessons on b.LessonID equals c.LessonID
                     where a.UserID == UserID
                     select new VM_UserSkills
                     {
                         LessonName=c.LessonName,
                         ProblemSolving=b.ProblemSolving,
                         MakingVideo=b.MakingVideo,
                         RelatedSoftware=b.RelatedSoftware
                     }).Distinct();
            return q.ToList();
        }

        public List<VM_UserLang> UserLang(int UserID)
        {
            var q = (from a in db.RegisterUser
                     join b in db.User_Lang on a.UserID equals b.UserID
                     join c in db.Language on b.LangID equals c.LangID
                     where a.UserID == UserID
                     select new VM_UserLang
                     {
                        LangName=c.LangName,
                        ScoreListening=b.ScoreListening,
                        ScoreReading=b.ScoreReading,
                        ScoreSpeking=b.ScoreSpeking,
                        ScoreWrite=b.ScoreWrite,
                        DescriptionLang=b.DescriptionLang
                        
                     }).Distinct();
            return q.ToList();
        }

        public List<VM_Company> UserCompany(int UserID)
        {
            var q = (from a in db.RegisterUser
                     join b in db.Company on a.UserID equals b.UserID
                     where b.UserID == UserID
                     select new VM_Company {
                         CompanyName=b.CompanyName,
                         Position=b.Position,
                         DescPosition=b.DescPosition,
                         DurationWork=b.DurationWork
                     }).Distinct();
            return q.ToList();

        }
    }
}