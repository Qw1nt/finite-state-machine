# Finite State Machine

## Основное

### Состояние

```csharp
// Стандартное состояние
public class Idle : StateBase
{
    public const ulong GlobalId = 1;

    public override ulong Id => GlobalId;

    public override IState Enter()
    {
        Debug.Log($"Enter to state {nameof(Idle)}");
        return this;
    }

    public override void Exit()
    {
        Debug.Log($"Exit from state {nameof(Idle)}");
    }
}
```

- #### Создание билдера конечного автомата
  ```csharp
  var builder = new StateMachineBuilder();
  ```
- #### Добавление состояния 
  ```csharp
  // Добавление состояния через generic-аргумент
  builder.AddState<Idle>();
  
  // Добавление состояния с передачей экземпляра 
  builder.AddState<Running>(new Running(movementSpeed: 5f))
  ```
  
- #### Создание переходов между состояниями 
  ```csharp
  // Переход из состояния Idle в состояние Running
  builder.AddTransition<Idle, Running>();
  
  // Переход из состояния Running в состояние Idle
  builder.AddTransition<Running, Idle>();
 
  // Двусторонний переход. Эквивалентно вышеописанному коду
  builder.AddTwoWayTransition<Idle, Running>();
   ```
  
- #### Установка начального состояния
  ```csharp
  // С помощью ID состояния 
  builder.SetInitialState(Idle.GlobalId);
  
  // С помощью типа
  builder.SetInitialState<Idle>();
  
  // Через экземпляр
  builder.SetInitialState<Idle>(instance);
  ```
  
- #### Создание конечного автомата 
  ```csharp
  var stateMachine = builder.Build();
  ```
  
  > После вызова ```Build()``` автоматически вызовется ```Dispose()```. Второй вызов ```Build()``` приведёт к ошибке

## Пример

```csharp
using Qw1nt.FiniteStateMachine.Runtime.Base;
using Qw1nt.FiniteStateMachine.Runtime.Common;
using Qw1nt.FiniteStateMachine.Runtime.Extensions;
using Qw1nt.FiniteStateMachine.Runtime.Interfaces;
using UnityEngine;

public class StateMachineExample : MonoBehaviour
{
    private StateMachine _stateMachine;
        
    private void Awake()
    {
        // Создание билдера
        var builder = new StateMachineBuilder();
     
        // Регистрация состояний   
        builder.AddState<Idle>();
        builder.AddState<Running>();
        builder.AddState<Healing>();
            
        // Регистрация переходов между состояниями
        builder.AddTwoWayTransition<Idle, Running>();
        builder.AddTwoWayTransition<Idle, Healing>();
            
        builder.AddTransition<Running, Healing>();

        // Установка начального состояния
        builder.SetInitialState(Idle.GlobalId);
            
        // Создание конечного автомата
        _stateMachine = builder.Build();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0) == true)
            _stateMachine.Switch(Idle.GlobalId);
            
        if(Input.GetKeyDown(KeyCode.Alpha1) == true)
            _stateMachine.Switch(Running.GlobalId);
            
        if(Input.GetKeyDown(KeyCode.Alpha2) == true)
            _stateMachine.Switch(Healing.GlobalId);
        
        _stateMachine.Update(Time.deltaTime);
    }
        
    public class Idle : StateBase
    {
        public const ulong GlobalId = 1;

        public override ulong Id => GlobalId;

        public override IState Enter()
        {
            Debug.Log($"Enter to state {nameof(Idle)}");
            return this;
        }

        public override void Exit()
        {
            Debug.Log($"Exit from state {nameof(Idle)}");
        }
    }      
        
    public  class Running : StateBase
    {
        public const ulong GlobalId = 2;

        public override ulong Id => GlobalId;

        public override IState Enter()
        {
            Debug.Log($"Enter to state {nameof(Running)}");
            return this;
        }

        public override void Exit()
        {
            Debug.Log($"Exit from state {nameof(Running)}");
        }
    }        
        
    public  class Healing : StateBase
    {
        public const ulong GlobalId = 3;

        public override ulong Id => GlobalId;

        public override IState Enter()
        {
            Debug.Log($"Enter to state {nameof(Healing)}");
            return this;
        }

        public override void Exit()
        {
            Debug.Log($"Exit from state {nameof(Healing)}");
        }
    }
}
```