using System;
using System.Collections.Generic;

// example input:
// % 7 3 b f b t bla // amount of clauses and goals and the searchmethod (here ’b’)
// p :- l, % comments can be at the end of lines like this
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


namespace Chaining {
	class Chaining {
		static void Main(string[] args) {
			string[] settings = Console.ReadLine().Split(' ');
			int m = int.Parse(settings[1]);
			int n = int.Parse(settings[2]);
			char t = char.Parse(settings[3]);
      // knowledge base as hashmap of rules
      Dictionary<string, string[]> kb = new Dictionary<string, string[]>();
			string[] queries = new string[n];
			// read knowledge base and parse
      for (int i = 0; i < m; i++) {
        // m rules
        string input = "";
        while (true){
          input += Console.ReadLine.Split('%')[0].Trim();
          if (input.Contains('.')) {break;}
        }
        string[] rule = input.Split(":-");
        string conclusion = rule[0].Trim();
        string[] premises = rule[1].Split(',').Trim();
        kb.Add(conclusion, premises);
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
