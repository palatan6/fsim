using System;
using System.Collections.Generic;

namespace ListViewWithChecking
{
    public delegate void OnAddAction(
        fsListViewWithChecking list,
        KeyValuePair<string, fsCheckItemInfo> namedState);

    public delegate void OnMultipleAddAction(
        fsListViewWithChecking list,
        List<KeyValuePair<string, fsCheckItemInfo>> namedStates);

    public delegate void OnRechargeAction(
        fsListViewWithChecking list,
        List<KeyValuePair<string, fsCheckItemInfo>> removeStates,
        List<KeyValuePair<string, fsCheckItemInfo[]>> commonStates,
        List<KeyValuePair<string, fsCheckItemInfo>> addStates);

    public delegate void OnRemoveAction(fsListViewWithChecking list, KeyValuePair<string, fsCheckItemInfo> namedState);

    public delegate void OnCheckAction(fsListViewWithChecking list, fsCheckItemInfo state);

    public delegate void OnMultipleCheckAction(fsListViewWithChecking list, List<fsCheckItemInfo> states);

    public delegate void OnDragDropAction(fsListViewWithChecking sourceList,
                                          fsListViewWithChecking targetList,
                                          fsDragDropItemInfo dragDropState);
}