Some shared classes I use in my .Net Core projects.

SharedMolecules.Core:
OperationResult: a container to return an Enum of the result and the object you want to return or you can just return an enum result.

SharedMolecules.EFCore.Context:
a context that track some details before you save the entity in the database: Details: 
|CreatedAt ,CreatedBy, LastUpdatedAt, LastUpdatedBy|

SharedMolecules.Entities: 

have the ITrackable that the context use and shared entity that contain generic Id and IsValid property

SharedMolecules.UI:

ViewRenderService a service return a component filled with it data

SharedMolecules.UnitTest.InMemory:

InMemory testing library that enables you to have your tests on memory so they become faster and don't have to delete and refill the data on each unit test.
