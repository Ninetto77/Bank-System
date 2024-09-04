using Lesson10;

namespace BankLogic_Library.workers.workerFactory
{
	static class WorkerFactory
	{
		public static IWorker GetWorker(WorkerType type)
		{
			switch (type)
			{
				case WorkerType.consultant: return new Consultate();
				case WorkerType.manager: return new Manager();
				default: return null;
			}
		}
	}
}
