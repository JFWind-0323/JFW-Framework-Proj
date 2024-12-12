  namespace Framework.Singleton
  {
      public abstract class Singleton<T> where T : Singleton<T>, new()
      {
          /*
           * 普通单例
           */
          protected static  T  instance;
          public static T Instance
          {
              get
              {
                  if (instance == null)
                  {
                      instance = new T();
                  }
                  return instance;
              }
          }
      }
  }