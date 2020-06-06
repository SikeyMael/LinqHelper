# LinqHelper
Linq and Dynamic Linq are very powerful. But sometimes you come across complicated commands to implement.
For example, you would expect that writing a left join is as simple as writing a join, instead you need to use the GroupJoin and SelectMany commands in sequence.

This extension was created with the intention of simplifying these operations, and the first implementation is to create an extension that adds the LeftJoin command to the IQueryable object.

# How to use
For code example look at tests implementation.
