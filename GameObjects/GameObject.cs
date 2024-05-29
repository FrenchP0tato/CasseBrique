using Microsoft.Xna.Framework.Graphics;

namespace CasseBrique.GameObjects
{
    public abstract class GameObject
    {
        private bool _enable;
        public Scene root {  get; private set; }
        public bool isFree=false;
        public bool enable
        {
            get { return _enable; }
            set {
                if(_enable != value)
                    {
                    _enable = value;
                    if (_enable) OnEnable();
                    else OnDisable();
                     }
                }
        }

        public GameObject(bool enable, Scene root)
        {
            _enable=enable;
            this.root = root;

        }

        protected virtual void OnEnable() { } // toutes les classes qui héritent y ont accès
        protected virtual void OnDisable() { }
        public virtual void Start() { }
        public virtual void OnFree() { }

        public virtual void Update(float dt) { }
        public virtual void Draw(SpriteBatch sb) { }

        

    }
}
