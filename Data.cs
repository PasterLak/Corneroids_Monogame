
public class Data
{

	public const byte CHUNK_SIZE = 16;  // max 255
	public static readonly byte CHUNK_SIZE_HALF = CHUNK_SIZE / 2;
	public static readonly int DELTA = ASTEROID_SIZE * Data.CHUNK_SIZE / 2;

	public const byte ASTEROID_SIZE = 64;   // max 255
	public const byte ASTEROID_SIZE_HALF = ASTEROID_SIZE / 2;
	public const int ASTEROID_SIZE_BLOCKS = ASTEROID_SIZE * CHUNK_SIZE;
	public const int ASTEROID_SIZE_BLOCKS_HALF = ASTEROID_SIZE_BLOCKS / 2;

	public const byte ASTEROIDS_COUNT_MIN = 1;
	public const byte ASTEROIDS_COUNT_MAX = 3;

	public const int SECTOR_SIZE = 8192;
	public const int SECTOR_SIZE_HALF = SECTOR_SIZE / 2;
	public const int INDENT_FROM_SECTOR_EDGE = ASTEROID_SIZE_BLOCKS;

	public const int SECTORS_IN_WORLD = 4;
	public const int SECTORS_IN_WORLD_HALF = SECTORS_IN_WORLD / 2;
	public const int WORLD_SIZE_BLOCKS = SECTORS_IN_WORLD * SECTOR_SIZE;

	public const float BLOCK_CENTER = 0.5f;

	public const float TIME_TO_REDRAW = 1f;
	public const float DRAW_CHUNK_DISTANCE = 50f;


	public static readonly float NOISE_SCALE = 14;  // 14

}
