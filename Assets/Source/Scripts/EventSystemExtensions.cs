using System.Collections.Generic;
using UnityEngine.EventSystems;

static class EventSystemExtensions
{
    public static bool CheckIsAnyElementPointed(this EventSystem eventSystem, PointerEventData eventData)
    {
        List<RaycastResult> raycastResults = new List<RaycastResult>();
        eventSystem.RaycastAll(eventData, raycastResults);
        return raycastResults.Count != 0;
    }
}