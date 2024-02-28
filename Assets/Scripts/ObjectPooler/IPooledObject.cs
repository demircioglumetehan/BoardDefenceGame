namespace BoardDefenceGame.ObjectPooler
{
    public interface IPooledObject
    {
        public void OnDestroyObject();
        public void OnSpawnObject(ObjectPooler generatedPooler);
    }
}
