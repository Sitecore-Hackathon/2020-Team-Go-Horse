# Usage Instructions

**Category:** Sitecore Marketplace Website

## Summary

This module was designed to solve one of the biggest problems with the old Sitecore Marketplace Website: **Content Duplication.**

Module Authors usually add documentation to the Git Repository itself, so why should them duplicate all that content 
in Marketplace, and keep it manually updated? 

Instead of copy/pasting, this module allows to link modules directly with their respective Git Repositories. 
GitHub WebAPI is then used to keep the Module Page in Sitecore always updated with the latest 
and greatest information from the Git Repository.

![Module fields](images/modulefields.jpg?raw=true "Module fields") 


## Content Tree

In a Vanilla Sitecore 9.3, this is how your content tree should look after the installation:

![Content Tree](images/content-tree-home.jpg?raw=true "Content Tree") 

**1. Marketplace** - The homepage - has a search box for users to find and browse Module Pages

**2. Results** - Shows a list of modules, filtered by the search keyword input by the user

**3. Modules** - Bulk repository of Sitecore modules

![Modules folder](images/content-tree-modules.jpg?raw=true "Modules folder") 


## Module Page

The Module Page is the heart of this module. Because it is directly connected to the Git Repository, this page saves time 
and avoids content redundancy.

![Module fields](images/modulefields.jpg?raw=true "Module fields") 


### Module Title Rendering

Has a little bit more than the title itself.

![Module Title](images/module-title.jpg?raw=true "Module Title") 

1. Module Name
2. Repository link
3. Module description
4. Download button



### Downloads Rendering

List of downloads is dynamically populated from the Git Repository. 

![Downloads](images/module-downloads.jpg?raw=true "Downloads") 

The module will scan the folder **/sc.package** (at root level) and build the download list.



### Documentation Rendering

The documentation is also populated from the Git Repository. 

![Documentation](images/module-documentation.jpg?raw=true "Documentation") 

**1. First tab (About)** is populated from the /README.md (root level)
**2. Rest of the tabs** are dynamically populated from the folder **/documentation/** (only *.me files are displayed)



### Contributors Rendering

A list of contributors is also populated from the Git Repository. 

![Contributors](images/module-contributors.jpg?raw=true "Contributors") 



## Video

For a quick introduction to this module, check this [Video Presentation](https://youtu.be/VJeheZ4DE08)