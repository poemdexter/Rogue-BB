using System.Collections.Generic;

public class MoveInput
{
	float timestamp;
	
	public bool right = false;
	public bool left = false;
	public bool jump = false;
	
	public MoveInput() {}
	
	public MoveInput(bool _left, bool _right, bool _jump) 
	{
		left = _left;
		right = _right;
		jump = _jump;
	}
	
	public bool gotInput()
	{
		return left || right || jump;
	}
	
	public bool[] getPacket()
	{
		List<bool> boolList = new List<bool>();
		boolList.Add(left);
		boolList.Add(right);
		boolList.Add(jump);
		return boolList.ToArray();
	}
	
	public static MoveInput getMoveInput(bool[] array)
	{
		return new MoveInput(array[0], array[1], array[2]);
	}
}
