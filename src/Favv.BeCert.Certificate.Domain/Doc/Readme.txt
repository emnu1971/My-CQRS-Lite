
--------------------------------------------
Requirement of the Certificate Domain Model:
--------------------------------------------

Our requirement is to implement the (minimal) creation of a Certificate and possible Annexes.
-Certificate may have zero or many Annexes. 
-A single Annex belongs to a specific Certificate at a certain moment in time.
-If an Annex exists, it must belong to a Certificate.
-Annex may be switched between Certificate, but they can not be assigned to multiple Certificate at the same time.
-Each Certificate and Annex must have a simple to remember unique ID.

---------
Commands:
---------

-CreateCertificate
-CreateAnnex
-AssignAnnexToCertificate
-RemoveAnnexFromCertificate

-------
Events:
-------

Changes in our system are presented by events and are stored in the "Write Model" data store.
Next are the possible events for our use case:
-CertificateCreated
-AnnexCreated
-AnnexAssignedToCertificate
-AnnexRemovedFromCertificate

------------------------
Building the Write Model
------------------------
The first part to build is the "domain" of the application.
In CQRS, the domain is a collection of objects to represent
the "WriteModel" and a collection of obejcts to represent the
"ReadModel" (having 2 separate models is one of the core requirements of CQRS).

In terms of DDD (Domain Driven Design), we will create an "Aggregate Root" to represent
our domain (in our sample this is simple, we'll have a single Aggregate Root).
The Aggregate Root is the WriteModel object through which all modifications are done.

In addition to the "Aggregate Root" (CertificateItem in our case), the "Write Model"
will also contain next items:

-Commands/CommandHandlers
-Events/EventHandlers (*)

Order of operations in the "Write Model":

1. A command is issued.
2. A commandhandler processes the command and builds/changes the correct Aggregate Root.
3. The build/changed Aggregate Root creates an Event and sends it to the "EventBus".
4. The Event Store saves the new Event.
5. The EventHandlers process the new event (to update the Read Model).


-----------------------
Building the Read Model
-----------------------

The ReadModel for any given object consists of:

-EventHandlers (*)
-ReadModelAccess

(*)EventHandlers are used to sync the "Read Data Store" with the "Write Data Store".
EventHandlers are both Write/Read model related.








