namespace FitnessTracker.Models
{
	public class VoedingPage
	{
		public IList<Voeding> _Voeding { get; set; }
		public Voeding Voeding { get; set; }
		public VoedingPage(IList<Voeding> _Voeding)
		{
			this._Voeding = _Voeding;
		}
	}
}
