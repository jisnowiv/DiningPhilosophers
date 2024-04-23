using System;
using System.Collections.Generic;
using System.Threading;

namespace DiningPhilosophers
{
	class Program
	{
		private const int PHILOSOPHER_COUNT = 5;

		static void Main(string[] args)
		{
			var philosophers = InitializePhilosophers();

			Console.WriteLine("Dinner is starting!");

			var philosopherThreads = new List<Thread>();
			foreach (var phil in philosophers)
			{
				var philThread = new Thread(new ThreadStart(philosopher.EatAll));
				philsopherThreads.Add(philThread);
				philThread.Start();
			}

			foreach (var thread in philosopherThreads)
			{
				thread.Join();
			}

			Console.WriteLine("Dinner is over!");
		}

		private static List<Philosopher> InitializePhilosophers()
		{
			var philosophers = new List<Philosopher>(PHILOSOPHER_COUNT);

			for (int i = 0; i < PHILOSOPHER_COUNT; i++)
			{
				philosophers.Add(new Philosopher(philosophers, i));
			}

			foreach (var philosopher in philosophers)
			{
				philosopher.LeftChopstick = philosopher.LeftPhilosopher.RightChopstick ?? new Chopstick();
				philosopher.RightChopstick = philosopher.RightPhilosopher.LeftChopstick ?? new Chopstick();
			}

			return philosophers;
		}
	}

	public class Philosopher
	{
		private const int TIMES_TO_EAT = 5;
		private int _timesEaten = 0;
		private readonly List<Philosopher> _allPhilosophers;
		private readonly int _idx;

		public Philosopher(List<Philosopher> allPhilosophers, int idx)
		{
			_allPhilosophers = allPhilosophers;
			_idx = idx;
			this.Name = string.Format("Philosopher {0}", _idx);
			this.State = State.Thinking;
		}

		public string Name { get; private set; }
		public State State { get; private set; }
		public Chopstick LeftChopstick { get; set; }
		public Chopstick RightChopstick { get; set; }

		public Philosopher LeftPhilosopher
		{
			get
			{
				if (_idx == 0)
					return _allPhilosophers[_allPhilosophers.Count - 1];
				else
					return _allPhilosophers[_idx - 1];
			}
		}

		public Philosopher RightPhilosopher
		{
			get
			{
				if (_idx == _allPhilosophers.Count - 1)
					return _allPhilosophers[0];
				else
					return _allPhilosophers[_idx + 1];
			}
		}

		public void EatAll()
		{
			while (_timesEaten < TIMES_TO_EAT)
			{
				this.Think();
				if (this.PickUp())
				{
					this.Eat();

					this.PutDownLeft();
					this.PutDownRight();
				}

			}
		}

		private bool PickUp()
		{
			return this.LeftChopstick.grab(this.Name) && this.RightChopstick.grab(this.Name);
		}

		private void Eat()
		{
			this.State = State.Eating;
			_timesEaten++;
			Console.WriteLine("{0} eats.", this.Name);
		}

		private void PutDownLeft()
		{
			this.LeftChopstick.release(this.Name);
		}

		private void PutDownRight()
		{
			this.RightChopstick.release(this.Name);
		}

		private void Think()
		{
			this.State = State.Thinking;
		}

	}

	public enum State
	{
		Thinking = 0,
		Eating = 1
	}

	public class Chopstick
	{
		private static int _count = 1;
		public string Name { get; private set; }
		public Mutex mtx { get; private set; }

		public bool grab(string philosopher) {
			Console.WriteLine("Philosopher {0} is waiting for {1}", philosopher, this.Name);
			this.mtx.WaitOne();
			Console.WriteLine("Philosopher {0} has picked up {1}", philosopher, this.Name);
			return true;
		}

		public void release(string philosopher) {
			this.mtx.ReleaseMutex();
			Console.WriteLine("Philosopher {0} has released {1}", philosopher, this.Name);
		}

		public Chopstick()
		{
			this.Name = "Chopstick " + _count++;
			this.mtx = new Mutex();
		}
	}
}