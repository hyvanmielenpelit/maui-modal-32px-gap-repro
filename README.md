# .NET MAUI Bug Reproduction: Modal Page 32px Title Bar Gap on Windows

## Bug Summary

On Windows, when a modal page is pushed via `Navigation.PushModalAsync()`, there is a **32-pixel tall gap at the top of the modal page** where the underlying page is visible through. Additionally, the top ~32px area is **unclickable** — mouse clicks are intercepted by the OS-level title bar non-client region.

## How to Reproduce

1. Open the solution in Visual Studio or run with `dotnet build -t:Run -f net10.0-windows10.0.19041.0`
2. **Maximize the window** (or make it full-screen) — the bug is most visible in this mode
3. Click **"Push Modal Page"**
4. Observe:
   - **BUG 1**: A blue strip (~32px) from the MainPage is visible at the very top of the window
   - **BUG 2**: The yellow button at the top says "Try clicking me!" — clicking near the top edge of the window does not register (the click counter does not increment). Clicks further down on the same button work fine.

## Expected Behavior

- The red modal page should cover the entire window, including the title bar area
- All areas of the modal page should be clickable

## Actual Behavior

- A ~32px gap at the top reveals the blue MainPage underneath
- The top ~32px is unclickable — intercepted by the WinUI non-client title bar region

## Root Cause

The `NavigationRootManager` constructor (`src/Core/src/Platform/Windows/NavigationRootManager.cs`) unconditionally reserves 32px for the window title bar on every `WindowRootView`, including those created for modal pages. The `ModalNavigationManager.Windows.cs` hides the title bar on the **previous** page but does **not** suppress it on the **modal's own** `WindowRootView`.

## Environment

- .NET MAUI (specify your version via `dotnet workload list`)
- Windows 10 / Windows 11
- Tested in maximized and full-screen window modes
