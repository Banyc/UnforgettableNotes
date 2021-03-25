# UnforgettableMemo

📒 A sticky note APP that shows sticky notes based on memory decay theory. 🧠 Least time for more remembering tasks! ⚡

![](img/screenshot.mainWindow.png)

## Features

-   It only shows **the least remembered sticky note**.
-   **The window pops up** when there is a sticky note somewhat **forgotten**.
-   The sticky note saves when the text is changed.
-   The window could not be deactivated but could be covered by other windows if every sticky note has not been forgotten.
-   When the review button (✔️) is clicked, the memory status updates.
-   Every 30 min the displaying sticky note is swapped to the least remembered one.

## How to Use

-   Write a sticky note in the middle textbox.
-   Click the top-left "+" button to add a new sticky note
-   Sticky notes are saved in realtime.
-   Click the bottom-left "✔️" button to review the displaying sticky note and swap to the other least remembered sticky note.
-   Click the bottom-left "🗑" button to remove the displaying sticky note.
-   Click the top-right "🔄" button to swap to the other least remembered sticky note.
-   Click the top-right "X" button to permanently close the application.

## Install Desktop Client

1. Download the executable from the release page
1. Schedule a startup for the executable

## Tips

-   Don't click the review button ("✔️") unless you have 100% confidence in remembering the sticky note at that point.
-   Click the delete button ("🗑") if the sticky note is no longer needed or fully remembered.
-   Don't click the exit button ("X") if you don't want to **permanently** exit the APP. If you want it to pop up next time, simply switch to your working software.

## Memory Decay Theory

The logic is based on the forgetting curve, mainly from this website <https://supermemo.guru/wiki/Forgetting_curve>.

![retrievability](img/retrievability-day.png)

> Forgetting curve describes the decline in the probability of recall over time:
>
> <img src="https://render.githubusercontent.com/render/math?math=R = e ^ {-t / S}"/>
>
> where:
>
> R - probability of recall (retrievability of memory)
>
> S - strength of a memory trace (stability of memory)
>
> t - time

![stabilityIncrease](img/stabilityIncrease-day.png)

> The formula for the stabilization curve:
>
> <img src="https://render.githubusercontent.com/render/math?math=SInc = SInc_{Max} * e ^ {-Gain * R}"/>
>
> where:
>
> SInc: stabilization (aka stability increase) is the ratio of memory stability after and before the repetition
>
> SIncMax: maximum possible increase in memory stability. SIncMax depends on current stability and memory complexity (i.e. item difficulty). Maximum stabilization can be achieved at the verge of forgetting
>
> Gain: a constant that expresses the gain in stabilization with passing time (gain is a measure of the spacing effect)
>
> R: retrievability, i.e. the expected probability of recall at a given point in time. It is determined by the forgetting curve

## Recommended Usage

-   Recite vocabularies

## Sticky Note Persistence

Logic goes [here](src/UnforgettableMemo.Shared/Data).

For fast development, the sticky notes are currently stored as JSON.

## TODO

-   [x] Introduce energy.
-   [x] Introduce cooling time between sticky notes.
-   [ ] Support social media bot
-   [ ] Desktop applications support cross-platform
-   [ ] Sync on cloud
