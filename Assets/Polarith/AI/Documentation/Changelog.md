Changelog
=========

[v1.4] Pathfinder
-----------------

### Changes

+ **[Pro]** Added AIMFollowWaypoints behaviour to follow a path or an arbitrary collection of points
+ **[Pro]** Added AIMLinearPath for quickly creating your own paths or patrols
+ **[Pro]** Added AIMUnityPathfinding, an adapter for Unity's NavMesh system so that AIMFollowWaypoints can utilize these information
+ **[Pro]** Extended overall API such that the AIMFollowWaypoints behaviour can be easily used with your custom Pathfinding solution

### Enhancements

+ Improved custom editors, they now follow Unity's standard with respect to multi-selection and prefab emphasizing

### Fixes

+ Fixed a bug which broke the multi-selection capabilities of custom editors (if you implemented your own editor based on AIMBehaviourEditor, you need to override the BehaviourProperty)
+ Fixed a bug where Unity crashed on calling Polarith AI API methods in a specific order (AIMContext.ResizeObjectives, thanks @Simon for pointing this out)
+ Fixed a bug in AIMContext which caused the indicator gizmo to flicker based on the set UpdateFrequency
+ Fixed the PlaneGizmo such that it is consistent with our other gizmos


[v1.3] Free versus Pro
----------------------

### Changes  

+ Polarith AI is now Polarith AI Pro, furthermore, we now provide a Free version in the store as well
+ Adapted the documentation for the new Free and Pro versions
+ Added source code of our controller components
+ **[Pro]** Every Pro component now have a Pro label icon

### Enhancements

+ **[Pro]** AIMSeekNavMesh, AIMFleeNavMesh: Improved editors, they now show a proper bitfield for AreaMask instead of just an integer

### Fixes  

+ Fixed a bug in our custom editor system which made proper multi-object editing impossible
+ Fixed a bug in character controller editors which caused the value of ObjectiveAsSpeed to be reset at runtime
+ AIMPhysicsController2D: Removed the unnecessary dependency to Rigidbody2D so that it can be decoupled properly from the actual physics object


[v1.2.5] Asynchronous Load Balancing
------------------------------------

### Changes

+ AIMContext: The Spacing in the indicator settings is now constraint to a minimum of 1

### Enhancements

+ AIMContext: Improved UI look and feel
+ AIMPlanarShaper: Improved UI look and feel
+ AIMThreading: Advanced asynchronous load balancing
+ AIMFollow, AIMArrive, AIMOrbit, AIMAlign: Added tags for assigning game objects
+ Documentation: Added syntax highlighting support for code snippets
+ Examples: Added scene for demonstrating the functionality of AIMLodGroup

### Fixes

+ AIMContext: Fixed a bug in the wizard where the default perceiver was not correctly detected
+ AIMContext: The Custom Scale in the indicator visualization of the result direction is now considered correctly
+ AIMSeekNavMesh: Minor UI improvements
+ Documentation: Minor corrections (links etc.)


[v1.2] Usability Boost
----------------------

### Changes

+ AIMStabilization: The last decision is used as reference direction instead of the last direction the agent faced

### Enhancements

+ Brand-new and extensible inspector interface which boosts up the general workflow
+ Components now support correct help URLs which reference to the online documentation
+ General usability improvements making the general workflow less error-prone (parameter checks, info boxes etc.)
+ Added level-of-detail component AIMLodGroup supporting automatic sensor switching for different distances
+ Added new avoidance component AIMAvoid which is an improvement and generalization of the planar version (it supports full 3D sensors in the future)

### Fixes

+ The objective normalization has no discontinuities anymore
+ Vector projection of OBBs was not working correctly for certain cases, but now it does
+ The bounds of a percept are now correctly extracted (before, they were in wrong space for certain cases)
+ AIMArrive: Gizmos in 3D were not visualized correctly, but now they are
+ AIMSimpleController: Fixed an error which could occur by setting an ObjectiveAsSpeed


[v1.1.2] The Fix of Shame
-------------------------

### Changes

+ Removed everything concerning the path structure until it is polished (we found some serious bugs to be present and decided to re-publish it later, a big SORRY)

### Fixes

+ Repaired gizmo functionality


[v1.1.1] Critical Hotfix
------------------------

### Fixes

+ Fixed critical bug preventing the build of full application executables


[v1.1] Path Structure
---------------------

### Changes

+ Polarith AI now requires at least Unity 5.3.0

### Enhancements

+ Path Structure: Added component AIMLinearPath for easily creating and modifying linear paths
+ Behaviour: Added AIMFollowPathDiscrete and AIMFollowPathContinuous for letting agents follow or patrolling on paths, e.g. an AIMLinearPath
+ Colors: Added static class for collecting all default colors used by Polarith

### Fixes

+ AIMSteeringFilter: Set the default value of ObjectTag to "Untagged" for preventing broken setups
+ AIMSteeringFilter: Fixed an issue where an exception was thrown if an AIMSteeringPerceiver was not attached
+ AIMPursue: Fixed Typo in AIMPursue (in Unity's component menu, it was named AIMPursu)


[v1.0.1] Release Bug Fixing
---------------------------

### Enhancements

+ AIMEnvironment can now use Unity's layer system to define perceived objects.
+ It is now possible to change the sensor's planar orientation in AIMPlanarShaper.
+ AIMContext indicator now considers the object scale.
+ Eliminated a remaining GetComponent call in the main update loop which caused 500 KByte of GC in editor mode.
+ Added a hierarchical distance check for the boundary behaviours to improve both performance and precision. Agents now use a fast approximation to check whether an object is relevant, and if it is, a more expensive but precise method is applied.
+ The general performance was improved to a noticeable degree.
+ Introduced new API methods for easily accessing epsilon constraints directly via AIMContext.

### Fixes

+ The AIMPlanarInterpolator did not correctly consider if a target objective is to be minimized or maximized.
+ Using static game objects led to major problems when using behaviours which need visual bounds information.
+ Spread parameter in AIMPlanarSeekBounds and AIMPlanarFleeBounds did not work properly.
+ Eliminated an exception caused by the AIMContext indicator in the editor when using ShowReceptors.
+ Removed obsolete parameter (DistanceMapping) in AIMOrbit.
+ Fixed a bug in AIMContext leading to an unhandled exception while the minus button was pushed and there were no objectives.
