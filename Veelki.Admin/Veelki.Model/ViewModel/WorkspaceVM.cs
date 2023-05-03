using Veelki.Data.Entities;
using System;
using System.Collections.Generic;

namespace Veelki.Model.ViewModel
{
    public class WorkspaceVM
    {
        public int id { get; set; }
        public string name { get; set; }
        public int created_by { get; set; }
        public DateTime created_date { get; set; }
        public bool isactive { get; set; }
        public int? modified_by { get; set; }
        public DateTime? modified_date { get; set; }
        public bool? isdeleted { get; set; }
        public List<Forms> formsList { get; set; }
        public List<WorkspacePermissonMapping> workspacePermissionMappings { get; set; }
    }
    public class WorkspacePagingVM
    {
        public List<WorkspaceVM> workspaceVMs { get; set; }
        public int PreviousPage { get; set; }
        public int NextPage { get; set; }
        public int TotalRecord { get; set; }
        public string ShowPageInfo { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Start { get; set; }
        public int End { get; set; }
    }
}
