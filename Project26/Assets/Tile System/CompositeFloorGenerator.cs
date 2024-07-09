public class CompositeFloorGenerator<T> : FloorGenerator<T> where T : Tile   {
	FloorGenerator<T> baseGenerator;
	FloorDecorator<T>[] decorators;

	public CompositeFloorGenerator(FloorGenerator<T> baseGenerator, params FloorDecorator<T>[] decorators) {
		this.baseGenerator = baseGenerator;
		this.decorators = decorators;
	}

	public Floor<T> Generate(int seed)
	{
		Floor<T> floor = baseGenerator.Generate(seed);
		foreach (var decorator in decorators)
		{
			floor = decorator.Decorate(seed, floor);
		}
		return floor;
	}
}