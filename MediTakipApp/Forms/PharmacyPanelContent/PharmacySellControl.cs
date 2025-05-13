using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediTakipApp.Forms.PharmacyPanelContent
{
    public partial class PharmacySellControl : UserControl
    {
        string connStr = "Server=202.61.227.225,1433;Database=metidata;User Id=metidata_user;Password=1q2w3e4r.;Encrypt=False;TrustServerCertificate=True;";

        public PharmacySellControl()
        {
            InitializeComponent();
        }
    }
}
