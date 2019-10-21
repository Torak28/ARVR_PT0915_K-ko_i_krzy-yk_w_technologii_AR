# Workflow

We will use typical github workflow:

1. Create a branch from the repository

__Commands used:__

```
git checkout -b my_branch
git push origin my_branch
```

The `-b` near `git checkout` creates new branch. If You want just to switch to branch You can just `git checkout branch_name`

2. Create, edit, rename, move, or delete files.

__Commands used:__

```
git add --all
git commit -am 'Some info'
git push origin all/app_design
```

3. Send a pull request from your branch with your proposed changes to kick off a discussion.

![nic](https://i.imgur.com/ZIegmv7.gif)

4. Make changes on your branch as needed. Your pull request will update automatically.
5. Merge the pull request once the branch is ready to be merged.
6. Tidy up your branches using the delete button in the pull request or on the branches page.