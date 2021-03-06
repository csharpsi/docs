﻿# Bundle: Versioning

The versioning bundle will create snapshots for every document upon every update made to it, or when it is deleted. It is useful when you need to track history of documents, or when you need a full audit trail.

## Installation

To activate versioning server-wide just add `Versioning` to `Raven/ActiveBundles` configuration in global configuration file or setup new database with compression bundle turned on using API or Studio.

How to create a database with compression enabled using Studio can be found [here](../../../studio/bundles/versioning).

## Configuration

By default, the Versioning bundle will track history for all documents and never purge old revisions. This is easily configurable by creating or changing configuration documents like this:

{CODE versioning1@Server\Extending\Bundles\Versioning.cs /}

In this example is a global configuration document, telling the versioning bundle to version all documents (`Exclude=false`), and to only keep up to 5 revisions - purging older ones (`MaxRevisions=5`).

It is possible to override the behavior of the Versioning Bundle for a set of documents by creating a document specifically for an entity name. For example, let us say that we don't want to version users, we can tell the Versioning Bundle to avoid doing so adding a configuration document that is specific to the `Users` collection:

{CODE versioning2@Server\Extending\Bundles\Versioning.cs /}

The naming convention used is `"Raven/Versioning/" + entityName`, where the entity name is the value of the Raven-Entity-Name property on the document.

Conversely, we can also set the default configuration to not version (`Exclude = false`), and configure versioning for each set of documents individually, by adding a document for each collection that we do want to version.

## How it works

With the Versioning Bundle installed, let us execute this code:

{CODE versioning3@Server\Extending\Bundles\Versioning.cs /}

If we inspect the server, we will see the following documents were created:

![Figure 1: Versioned Documents](images\versioned_docs.png)

The first document is the actual document that we just saved, we can see that it has a few additional metadata property than the ones we are used to:

* Raven-Document-Revision - The document revision (starts at 1).
* Raven-Document-Revision-Status - Whatever it is the Current revision or a Historical snapshot.

Now, let us modify the original document. This will give us:

![Figure 2: Versioned Documents, Modified](images\versioned_docs_2.png)

As you can see, we have full audit record of all the changes that were made to the document.

You can access each of the revisions by simply using its id "users/1/revisions1". However, modifications / deletions of revisions are not allowed, and will result in an error if attempted.

Now, let us delete the original document:

![Figure 3: Versioned Documents, Deleted](images\versioned_docs_3.png)

That removed the current document, but the snapshots will remain in place, so you aren't going to lose the audit trail if the document is deleted. 

As you can see, the Versioning Bundle attempts to make things as simple as possible, and once it is installed, you'll automatically get the appropriate audit trail. Working with revisions is identical to working with standard documents (except that you can't modify / delete them). Revision documents cannnot be indexed like standard documents.

Limitations

* Versioning will fail on documents with keys larger than 230 characters.
* Versioning will not attempt to version internal Raven document (documents whose key starts with "Raven/")

## Client integration

The Versioning bundle also have a client side part, which you can access by adding a reference to Raven.Client.Versioning assembly.

You can then access past revisions of a document using the following code:

{CODE versioning_4@Server\Extending\Bundles\Versioning.cs /}
