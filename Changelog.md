
---

**1.1.7** - June 24 2010

---


-Fix: Authorization using headers instead of cookies. Fixes exception when opening preferences, or just not getting unread count.


---

**1.1.6** - May 3 2010

---


  * Fix: Exception when a Google Apps account exists with the same login data


---

**1.1.5** - January 6 2010

---


  * Fix: exception when browser list has an invalid entry
  * Feature: added a choice between taskbar icons for unread items.


---

**1.1.4** - October 1 2009

---


  * Fix: Changed login method, to fix Google login issues. Hope it helps.


---

**1.1.3** - July 29 2009

---


  * Fix: Labels Refresh button now works when you change login info, no need to close and reopen Preferences anymore.
  * Feature: Automatic check for new version.
  * Change: Slight changes to Icons.


---

**1.1.2  - July 21 2009**

---


  * Fix: When changing resolution, Notifier launched Google Reader in browser. (regression in 1.1.1)


---

**1.1.1  - July 15 2009**

---


  * Feature: Better handling of tags / labels. You can choose from a list now, rather than writing them yourself.
  * Feature: Can play a  WAV file when there's new items.
  * Feature: Added support for [Snarl](http://www.fullphat.net/about/index.html) (notification app). If Snarl is running, notification will show in Snarl instead of the built-in popup window.
  * Change: Settings file moved from app folder, to fix Permission issues in windows 7 (and maybe others). existing settings will be kept.
  * Bunch of other little changes and bugfixes.


---

**1.0.8  - June 26 2009**

---


  * Fix: Link in popup notification always opened in default browser, even if another browser to use was specified in settings.


---

**1.0.8beta2  - June 14 2009**

---


  * Fix: Potential fix for some login issue. username and pass needed HTMLEncode.


---

**1.0.8beta  - June 12 2009**

---


  * Feature: Added choice between installed browsers (for "Go to Reader")


---

**1.0.7c - April 10 2009**

---


  * Fix: Google changed their login system.
  * Change: Pointed the Help link in About dialog to go to http://code.google.com/p/reader-notifier-mod/w/list


---

**1.0.7b - March 23 2009**

---


  * Fix: installer requires latest .NET


---

**1.0.7a - July 29 2008**

---


  * Fix: timer should work now


---

**1.0.7 - July 08 2008**

---

  * Change tooltip to notify "0 unread items" when there are no new items
  * Fix: the timer shouldn't stop when connection failed
  * Fix: exception when not connected to the internet
  * Added error message when not connected to the internet
  * Saving settings now works when error present (though the window won't colse)


---

**1.0.6**

---

  * Fixed authentication check


---

**SVN REV 10-12**

---

  * Added check for valid login when settings changed.
  * Added check for settings on app startup.
  * Added an option "Start With Windows"