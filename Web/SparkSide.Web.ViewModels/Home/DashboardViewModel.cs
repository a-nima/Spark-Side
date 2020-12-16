namespace SparkSide.Web.ViewModels.Home
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class DashboardViewModel
    {
        public DashboardViewModel()
        {
            Greeting = "Welcome";
            if (DateTime.Now.TimeOfDay.Hours >= 6)
            {
                Greeting = "Good morning";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 12)
            {
                Greeting = "Good afternoon";
            }
            else if (DateTime.Now.TimeOfDay.Hours >= 20 || DateTime.Now.TimeOfDay.Hours < 6)
            {
                Greeting = "Good evening";
            }
        }

        public string Greeting { get; set; }

        public string UserFirstName { get; set; }
    }
}
