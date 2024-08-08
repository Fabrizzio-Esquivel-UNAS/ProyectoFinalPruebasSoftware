import { BehaviorSubject, Observable } from "rxjs";

export class StateBase<T> {
    // create an internal subject and an observable to keep track
    protected stateItem = new BehaviorSubject<any>(null);
    stateItem$: Observable<T | null> = this.stateItem.asObservable();

    // shall move soon to state service
    setState(item: T) {
        // console.log("item: ",item);
        this.stateItem.next(item);
        return this.stateItem$;
    }
    updateState(item: Partial<T>) {
        const newItem = { ...this.stateItem.getValue(), ...item };
        // console.log("newItem: ", newItem)
        this.stateItem.next(newItem as T);
        return this.stateItem$;
    }
    removeState(id?: string) {
        this.stateItem.next(null);
    }
    getState(): T | null {
        return this.stateItem.getValue();
    }
}

export abstract class StateBaseList<T> extends StateBase<T> {
    override updateState(subItem: T) {
        // console.log("newSubItem: ",newSubItem)
        let exists = false;
        const newItem = this.stateItem.getValue()!.map((existingSubItem: any) => {
            if (existingSubItem.id == (subItem as any).id) {
                const newSubItem = { ...subItem, ...existingSubItem };
                exists = true;
                console.log("newSubItem:",newSubItem);
                return newSubItem;
            }
            return existingSubItem;
        });
        if (exists==false) {
            newItem.push(subItem);
        }
        this.stateItem.next(newItem);
        return this.stateItem$;
    }
    override removeState(id?: string) {
        if (id) {
            let newItem = this.stateItem.getValue()!
            const index = newItem.findIndex((x: any) => x.id == id)
            if (index>-1) {
                newItem.splice(index, 1);
            }
            this.stateItem.next(newItem);
            return
        }
        this.stateItem.next(null);
    }
    getStateItem(id: string) {
        const itemList = this.stateItem.getValue();
        // console.log("Tring to find '"+id+"' in ", itemList);
        if (itemList != null) {
            const index = itemList.findIndex((x: any) => x.id == id);
            return itemList[index];
        }
    }
}
