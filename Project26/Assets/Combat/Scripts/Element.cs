using System.Collections.Generic;

public enum Element {
	CLOBBER, FLAME, CHILL, ABRASE, SOAK, RADIATE, FLUX, COMMAND, ZETA
}
public static class ElementExtensions {
	private static readonly Dictionary<Element, string> names = new Dictionary<Element, string>() {
		{ Element.CLOBBER, "Clobber" },
		{ Element.FLAME, "Flame" },
		{ Element.CHILL, "Chill" },
		{ Element.ABRASE, "Abrase" },
		{ Element.SOAK, "Soak" },
		{ Element.RADIATE, "Radiate" },
		{ Element.FLUX, "Flux" },
		{ Element.COMMAND, "Command" },
		{ Element.ZETA, "Zeta" }
	};
	public static string Name(this Element element) => names[element];
}