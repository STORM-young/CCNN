public enum ItemType 
{
    //种子，商品，家具
    //工具(锄头，砍树，砸石, 割草，浇水，菜篮)
    //可被割的杂菜
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
//物体类型
public enum PartType
{
    None,Carry,Hoe,Break,Water,Collect,Chop,Reap
}
//身体各部分
public enum PartName
{
    Body,Hair,Arm,Tool
}
public enum Season
{
    春天,夏天,秋天,冬天
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