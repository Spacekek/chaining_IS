using System;
using System.Collections.Generic;

namespace Chaining {
	class Chaining {
    static void Main(string[] args) {
      string[] settings = Console.ReadLine().Split(' ');
      int m = int.Parse(settings[1]);
      int n = int.Parse(settings[2]);
      char t = char.Parse(settings[3]);
      Dictionary<string, List<string[]>> kb = Read_KB(m);
      string[] queries = new string[n];
      for (int i = 0; i < n; i++) queries[i] = Console.ReadLine().Split(' ')[1].Split('.')[0];
      Output(kb, queries, t);
    }

    // function for reading the knowledge base
    static Dictionary<string, List<string[]>> Read_KB(int m) {
      Dictionary<string, List<string[]>> kb = new Dictionary<string, List<string[]>>();
      for (int i = 0; i < m; i++) {
        // m rules
        string input = "";
        while (true){
          input += Console.ReadLine().Split('%')[0].Trim();
          if (input.Contains(".")) {break;}
        }
        // if the rule is only a fact (no premises), add "true" as premise
        if (!input.Contains(":-")) {
          input = input.Replace(".", ":- true.");
        }
        string[] rule = input.Split('-');
        // remove ':' from conclusion
        string conclusion = rule[0].Substring(0, rule[0].Length-1).Trim();
        string[] premises = rule[1].Split(',');
        for (int j = 0; j < premises.Length; j++) {
          premises[j] = premises[j].Trim();
          premises[j] = String.Join("", premises[j].Split('.', ' '));
        }
        if (!kb.ContainsKey(conclusion)) {
          kb[conclusion] = new List<string[]>();
        }
        kb[conclusion].Add(premises);
      }
      return kb;
    }

    // function
    static void Output(Dictionary<string, List<string[]>> kb, string[] queries, char t) {
			foreach (string query in queries) {
        if (t == 'b') {
          if (BackwardChaining(kb, query, new HashSet<string>()) || query == "true") {
            Console.WriteLine(query + ". " + "true" + '.');
          }
          else Console.WriteLine(query + ". " + "false" + '.');
        }
        else {
          if (ForwardChaining(kb, query, new HashSet<string>(), new HashSet<string>()) || query == "true") {
            Console.WriteLine(query + ". " + "true" + '.');
          }
          else Console.WriteLine(query + ". " + "false" + '.');
        }
      }
    }
    static bool ForwardChaining(Dictionary<string, List<string[]>> kb, string query, HashSet<string> facts, HashSet<string> seen) {
			if (query == "true") facts.Add(query);
			if (facts.Contains(query)) return true;
			if (seen.Contains(query)) return false;
			seen.Add(query);
			if (!kb.ContainsKey(query)) return false;
			foreach(KeyValuePair<string, List<string[]>> entry in kb) {
				foreach (string[] rule in entry.Value) {
					bool allPremisesTrue = true;
					foreach (string premise in rule) {
						if (!ForwardChaining(kb, premise, facts, seen)) {
							allPremisesTrue = false;
							break;
						}
						seen.Remove(premise);
					}
					if (allPremisesTrue) {
						facts.Add(entry.Key);
						break;
					}
				}
			}
			return facts.Contains(query);
		}
    static bool BackwardChaining(Dictionary<string, List<string[]>> kb, string query, HashSet<string> seen) {
			// if query is true, return true
			if (query == "true") return true;
			// if query has been seen, return false
			if (seen.Contains(query)) return false;
			seen.Add(query);
      // if query is not in knowledge base, return false
      if (!kb.ContainsKey(query)) return false;
      foreach (string[] rule in kb[query]) {
        bool allPremisesTrue = true;
        foreach (string premise in rule) {
          if (!BackwardChaining(kb, premise, seen)) {
						allPremisesTrue = false;
            break;
          }
					// remove premise from seen to allow for multiple uses of premise
					seen.Remove(premise);
        }
        if (allPremisesTrue) return true;
      }
      return false;
    }
    static List<string[]> Rules_For_Goal(Dictionary<string, List<string[]>> kb, string goal){
      kb.TryGetValue(goal, out List<string[]> rules);
      return rules;
    }
	}
}
