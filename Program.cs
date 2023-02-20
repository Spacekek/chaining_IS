using System;

namespace Chaining {
	class Chaining {
		static void Main(string[] args) {
			// example input:
			// % 7 3 b
			// p :- l, % example comment to be ignored
			// m.
			// m :- b, l.
			// q :- p.
			// b.
			// l :-
			// a,
			// p.
			// l :- a,
			// b.
			// a.
			// % s.
			// % q.
			// % p.
			string[] settings = Console.ReadLine().Split(' ');
			int m = int.Parse(settings[1]);
			int n = int.Parse(settings[2]);
			char t = char.Parse(settings[3]);
			string[] kb = new string[m];
			string[] queries = new string[n];
			for (int i = 0; i < m; i++) {
				// m clauses in the knowledge base
				while (true) {
					string s = Console.ReadLine();
					if (s.Contains("%")) {
						s = s.Split('%')[0];
					}
					kb[i] += s;
					if (s.Contains(".")) {
						break;
					}
				}
				// save facts as rules, E.G. a. becomes a :- true.
				if (!kb[i].Contains(":-")) {
					kb[i] = kb[i].Split('.')[0] + " :- true.";
				}
			}
			for (int i = 0; i < n; i++) {
				// n queries
				queries[i] = Console.ReadLine().Split(' ')[1].Split('.')[0];
			}

			// test if everything is read correctly
			Console.WriteLine("m = " + m + ", n = " + n + ", t = " + t);
			Console.WriteLine("Knowledge base:");
			foreach (string s in kb) {
		 		Console.WriteLine(s);
			}
			Console.WriteLine("Queries:");
			foreach (string s in queries) {
			 	Console.WriteLine(s);
			}

			// run chosen algorithm
			if (t == 'b'){
				BackwardChaining(kb, queries);
			}
			else {
				ForwardChaining(kb, queries);
			}
		}
		// forward chaining to find out which queries are true given the knowledge base
		static void ForwardChaining(string[] kb, string[] queries) {
		}

		// backward chaining to find out which queries are true given the knowledge base
		static void BackwardChaining(string[] kb, string[] queries) {
		}
	}
}
