using MCMAutomation.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCMAutomation.APIHelpers.NewAppAPI.User.Progress
{
    public partial class ProgressNewReq
    {
        public class ProgressModel
        {
            public string? CalorieTargetMet { get; set; }
            public string? PeriodAffectsWeight { get; set; }
            public string? StepGoalMet { get; set; }
            public string? SleepHours { get; set; }
            public string? HungerLevel { get; set; }
            public string? EnergyLevel { get; set; }
            public string? StressLevel { get; set; }
            public string? Weight { get; set; }
            public string? Waist { get; set; }
            public string? Hip { get; set; }
            public string? Thigh { get; set; }
            public string? Chest { get; set; }
            public string? Arm { get; set; }
            public string? FrontPhoto { get; set; }
            public string? BackPhoto { get; set; }
            public string? SidePhoto { get; set; }
            public string? MeasurementUnit { get; set; }
            public string? GoalId { get; set; }
            public string? TrackMacros { get; set; }

        }


        public class ProgressDailyModel
        {
            public string date { get; set; }
            public int weight { get; set; }
            public bool calorieTargetMet { get; set; }
            public bool stepGoalMet { get; set; }
            public int measurementUnit { get; set; }
        }


        public class ProgressModelResponse
        {
            public int? content { get; set; }
            public bool? isSuccess { get; set; }
            public int? responseCode { get; set; }
            public string? errorMessage { get; set; }
        }

        public static ProgressModel GenerateReq()
        {
            ProgressModel req = new();

            req.CalorieTargetMet = RandomHelper.RandomBool().ToString();
            req.PeriodAffectsWeight = RandomHelper.RandomBool().ToString();
            req.StepGoalMet = RandomHelper.RandomBool().ToString();
            req.SleepHours = RandomHelper.RandomNum(3).ToString();
            req.HungerLevel = RandomHelper.RandomNumFromOne(5).ToString();
            req.EnergyLevel = RandomHelper.RandomNumFromOne(5).ToString();
            req.StressLevel = RandomHelper.RandomNumFromOne(5).ToString();
            req.Weight = RandomHelper.RandomProgressData("Weight").ToString();
            req.Waist = RandomHelper.RandomProgressData("Waist").ToString();
            req.Hip = RandomHelper.RandomProgressData("Hip").ToString();
            req.Thigh = RandomHelper.RandomProgressData("Thigh").ToString();
            req.Chest = RandomHelper.RandomProgressData("Chest").ToString();
            req.Arm = RandomHelper.RandomProgressData("Arm").ToString();
            req.FrontPhoto = "";
            req.BackPhoto = "";
            req.SidePhoto = "";
            req.MeasurementUnit = RandomHelper.RandomNum(1).ToString();
            req.GoalId = "EA647374-19DA-4DD8-BB39-B83E09C850B0";
            req.TrackMacros = RandomHelper.RandomNum(1).ToString();

            return req;
        }

    }
}
