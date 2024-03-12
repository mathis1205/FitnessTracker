namespace FitnessTracker.Models
{
    public class WorkoutPage
    {
        public IList<Workout> Workouts { get; set; }
        public Workout Workout { get; set; }
        public WorkoutPage(IList<Workout> workouts)
        {
            Workouts = workouts;
        }
    }
}
