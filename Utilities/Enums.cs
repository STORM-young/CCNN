public enum ItemType 
{
    //���ӣ���Ʒ���Ҿ�
    //����(��ͷ����������ʯ, ��ݣ���ˮ������)
    //�ɱ�����Ӳ�
    Seed, Commodity,Furniture,
    HoeTool,ChopTool,BreakTool,ReapTool,WaterTool,CollectTool,
    ReapableScenery
}
public enum SlotType
{
    Bag, Box, Shop
}
public enum InventoryLocation
{
    Player, Box, Shop
}
//��������
public enum PartType
{
    None,Carry,Hoe,Break,Water,Collect,Chop,Reap
}
//���������
public enum PartName
{
    Body,Hair,Arm,Tool
}
public enum Season
{
    ����,����,����,����
}

public enum GridType
{
    Diggable,DropItem,PlaceFurniture,NPCObstacle
}

public enum ParticleEffectType
{
    None,LeaveFalling01, LeaveFalling02,Rock,ReapableScenery
}

public enum GameState
{
    Gameplay,Pause
}

public enum LightShift
{
    Morning,Night
}

public enum SoundName
{
    none, FootStepSoft, FootStepHard,
    Axe, Pickaxe, Hoe, Reap, Water, Basket,
    Pickup, Plant, TreeFalling, Rustle,
    AmbientCountryside1, AmbientCountryside2, 
    MusicCalm1, MusicCalm2, MusicCalm3, MusicCalm4, MusicCalm5, MusicCalm6,
    AmbientIndoor1
}