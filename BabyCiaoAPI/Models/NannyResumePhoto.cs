using System;
using System.Collections.Generic;

namespace BabyCiaoAPI.Models;

public partial class NannyResumePhoto
{
    public int Id { get; set; }

    public int IdNannyResume { get; set; }

    public string PhotoName { get; set; } = null!;

    public DateTime ModifiedTime { get; set; }

    public virtual NannyResume IdNannyResumeNavigation { get; set; } = null!;
}
