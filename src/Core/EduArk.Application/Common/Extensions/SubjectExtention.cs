using EduArk.Application.Common.Helper;
using EduArk.Application.DTOs.SubjectDTOs;
using EduArk.Domain.Entities.Tenant;
using EduArk.Domain.Enums;

namespace EduArk.Application.Common.Extensions
{
    public static class SubjectExtention
    {
        public static Subject ToEntity(this SubjectDTO subjectDto, Subject? subject = null)
        {
            if(subject is  null) subject = new Subject();

            subject.Id = subjectDto.Id;
            if(subject.Id is 0)
            {
                subject.Name = subjectDto.Name;
            }
           
            subject.SubjectCode = subjectDto.SubjectCode;
            subject.SubjectCategory = subjectDto.SubjectCategory;
            subject.ParentBasketSubjectId = subjectDto.ParentBasketSubjectId;
            subject.SubjectStreamId = subjectDto.SubjectStreamId;
            subject.IsActive = true;

            if (subjectDto.SubjectType == SubjectType.BasketSubject)
            {
                subject.IsBuscketSubject = true;
                subject.IsParentBasketSubject = false;
            }
            else if (subjectDto.SubjectType == SubjectType.ParentBasketSubject)
            {
                subject.IsParentBasketSubject = true;
                subject.IsBuscketSubject = false;
            }
            else
            {
                subject.IsBuscketSubject = false;
                subject.IsParentBasketSubject = false;
            }

            return subject;
        }

        public static SubjectDetailsDTO ToSubjectDetailsDTO(this Subject subject,  SubjectDetailsDTO? subjectDetailsDTO = null) 
        { 
            if(subjectDetailsDTO is null) subjectDetailsDTO = new SubjectDetailsDTO();

            subjectDetailsDTO.Id = subject.Id;
            subjectDetailsDTO.Name = subject.Name;
            subjectDetailsDTO.SubjectCode = subject.SubjectCode;
            subjectDetailsDTO.SubjectCategory = subject.SubjectCategory;
            subjectDetailsDTO.SubjectCategoryName = EnumHelper.GetEnumDescription(subject.SubjectCategory);

            if (subject.IsBuscketSubject == false && subject.IsParentBasketSubject == false)
            {
                subjectDetailsDTO.SubjectType = SubjectType.NormalSubject;
            }
            else if (subject.IsParentBasketSubject == true)
            {
                subjectDetailsDTO.SubjectType = SubjectType.ParentBasketSubject;
            }
            else
            {
                subjectDetailsDTO.SubjectType = SubjectType.BasketSubject;
            }
            subjectDetailsDTO.ParentBasketSubjectId = subject.ParentBasketSubjectId;
            
            subjectDetailsDTO.SubjectStreamId = subject.SubjectStreamId; 
            subjectDetailsDTO.SubjectStreamName = subject.SubjectStream.Name;
            subjectDetailsDTO.CreatedByName = subject.CreatedByUser.FirstName;
            subjectDetailsDTO.UpdatedByName = subject.UpdatedByUser.FirstName;
            subjectDetailsDTO.UpdatedDate = subject.UpdateDate.Value.ToString("MMM d, yyyy");
            subjectDetailsDTO.CreatedDate = subject.CreatedDate.ToString("MMM d, yyyy");

            var subjectAcademicLevels = subject.SubjectAcademicLevels.Where(x => x.SubjectId == subject.Id);

            foreach (var item in subjectAcademicLevels)
            {
                subjectDetailsDTO.SubjectAcademicLevels.Add(item.AcademicLevelId);
            }

            return subjectDetailsDTO;
        }
    }
}
