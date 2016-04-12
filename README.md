# DnxProjectExtended
A sample project demonstrating how to extend the functionality of DNX Project.

We want to extend the functionality of the DNX Project by adding some additional entities. Currently we only need
info for the views in the project. A view is a set of file resources such as html, css, js, etc. grouped together
and containing additional metadata.

Why creating a wrapper around the DNX project instead of just extending the class itself:
- DNX project file is loaded from the filesystem using a static method. This static method uses a dedicated reader
  which implements custom logic on DNX project file parsing;
- DNX project file couldn't be easily deserialized as some of the used types (SemanticVersion) doesn't have a default constructors;
- We want to reuse the code which loads the original DNX project file without copying it in our code. We will benefit
  from this if in future there are changes in the original DNX project source code;
- There is no save to file system functionality implemented;
...
