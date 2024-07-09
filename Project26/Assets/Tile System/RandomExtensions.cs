public record RandomIntBetween {
	private int min, max;
	public RandomIntBetween(int min, int max) {
		this.min = min;
		this.max = max;
	}
	public int Sample(System.Random rng) => rng.Next(min, max + 1);
}
public static class RandomExtensions {
	public static bool NextBool(this System.Random rng) => rng.Next(2) == 1;
}