  namespace Singleton
  {
      public abstract class Singleton<T> where T : Singleton<T>, new()
      {
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