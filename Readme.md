# The Approach

An amalgamation of the Object oriented approach and Functional approach to solve this particular problem

## The rationale

Conceptuallly the entire problem can be decomposed into just three classes The Clock, which is nothing but a collection of ClockRows which in turn has a collection of Lamps (or more precisely a collection of Lamp status values)

Each ClockRow whether it is Hour, Minute or Second is inherently the same object - they only differ in the number of lamps and their behaviour. Once we can isolate this difference in behaviour which is nothing but different mechanisms of lighting the lamps for an individual row, our object model becomes quite simple and elegant even.

The responsibility of the Clock class is then to simply distribute the respective time components to each ClockRow and set their individual behaviour. The advantage of this approach is that within the Clock class one can almost describe the clock construction, by declaratively building up each ClockRow.

And then each ClockRow is responsible for displaying itself. 
