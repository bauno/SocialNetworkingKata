Feature: Social networking
	In order to express my ego
	As an idiot
	I want to interact with other idiots
	

Scenario: Reading Alice's posts
	Given Alice posted "I love the weather today" to her wall 5 minutes ago
	When someone enters the command "Alice"
	Then he can read "I love the weather today (5 minutes ago)"


Scenario: Reading Bob's posts
	Given Bob posted "Damn! We lost" to his wall 2 minutes ago
	And Bob posted "Good game though" to his wall 1 minute ago
	When someone enters the command "Bob"
	Then he can read "Good game though (1 minutes ago)"
	Then he can read "Damn! We lost (2 minutes ago)"
	


Scenario: Subscriptions
	Given Charlie posted "I'm in New York today! Anyone wants to have a coffee?" 2 seconds ago
	And Charlie follows Alice
	And Alice posted "I love the weather today" 5 minutes ago
	When someone gives the command "Charlie Wall"
	Then he can read "I'm in New York today! Anyone wants to have a coffee? (2 seconds ago)"
	And he can read  "I love the weather today (5 minutes ago)"

Scenario: Multiple subscriptions	
	Given Bob posted "Damn! We lost" to his wall 2 minutes ago
	And Bob posted "Good game though" to his wall 1 minutes ago
	And Alice posted "I love the weather today" to her wall 15 minutes ago
	And Charlie posted "I'm in New York today! Anyone wants to have a coffee?" 15 seconds ago
	And Charlie follows Alice
	And Charlie follows Bob
	When someone enters the command Charlie wall
	Then he can read "Charlie - I'm in New York today! Anyone wants to have a coffee? (15 seconds ago)"
	And he can read "Bob - Good game though (1 minutes ago)"
	And he can read "Bob - Damn! We lost (2 minutes ago)"
	And he can read "Alice - I love the weather today" to her wall 15 minutes ago

