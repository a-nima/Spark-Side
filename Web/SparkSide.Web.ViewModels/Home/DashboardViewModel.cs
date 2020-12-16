namespace SparkSide.Web.ViewModels.Home
{
    using SparkSide.Services.Data.Models;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            this.Greeting = "Welcome";
            if (DateTime.Now.TimeOfDay.Hours >= 20 || DateTime.Now.TimeOfDay.Hours < 6)
            {
                this.Greeting = "Good evening";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 12)
            {
                this.Greeting = "Good afternoon";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 6)
            {
                this.Greeting = "Good morning";
            }
        }

        public string Greeting { get; set; }

        public UserDTO User { get; set; }
    }
}
