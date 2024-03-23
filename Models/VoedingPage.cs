namespace FitnessTracker.Models;

public class VoedingPage
{
	public VoedingPage(IList<Voeding> _Voeding)
	{
		this._Voeding = _Voeding;
	}

	public IList<Voeding> _Voeding { get; set; }
	public Voeding Voeding { get; set; }
}