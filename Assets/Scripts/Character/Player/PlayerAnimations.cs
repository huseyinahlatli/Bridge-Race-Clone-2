using Singleton;
using UnityEngine;

namespace Character.Player
{
    public class PlayerAnimations : Singleton<PlayerAnimations>
    {
        private Animator _animator;
        
        private static string Run { get; }
        private static string Dance { get; }
        
        private static readonly int IsRunning = Animator.StringToHash(Run = StringCache.IsRunning);
        private static readonly int IsDancing = Animator.StringToHash(Dance = StringCache.IsDancing);

        private void Start()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void KeepRunning()
        {
            if(!_animator.GetBool(IsRunning))
            {
                _animator.SetBool(Run, true);
            }
        }

        public void StopRunning()
        {
            _animator.SetBool(Run, false);
        }

        public void Dancing()
        {
            if (_animator.GetBool(IsDancing)) return;
            
            _animator.SetBool(Run, false);
            _animator.SetBool(Dance, true);
        }
    }
}
