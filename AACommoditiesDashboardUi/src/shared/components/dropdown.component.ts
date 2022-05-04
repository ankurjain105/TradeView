/**
 * Angular 2 decorators and services
 */
 import {
    Component,
    OnInit,
    AfterViewInit,
    ViewEncapsulation,
    Input,
    Output,
    EventEmitter,
    Optional,
    Inject,
    ViewChild,
    ContentChildren,
    QueryList,
    ElementRef,
    HostListener
  } from '@angular/core';
  
  import { animations } from "../../core";
  
  @Component({
    selector: 'dropdown',
    encapsulation: ViewEncapsulation.None,
    styleUrls: ['./dropdown.component.scss'],
    templateUrl: './dropdown.component.html',
    animations,
    host: {
      '(document:click)': 'documentClick($event)',
    },
    providers: [],
    inputs: ['items'] 
  })
  export class DropdownComponent  {
    constructor(private _eref: ElementRef) {
    }
  
    @Input() displayProperty: string = 'name';
    @Input() label: string = '';
    @Input() id: string = '';
    @Input() placeholder: string = '';
    @Input() items: any[]= [];
    @Input() selectedItem: any = null;
    @Output() change: EventEmitter<any> = new EventEmitter<any>();
    @Input() readonly: boolean = false;

    public identifier = `dropdown-${identifier++}`;
  
    selectedItemText: string = '';
    showResults: boolean = false;
    activeIndex: any;
    value: any;
  
    ngOnInit() {
      console.log('drop down init', this.value);
    }
  
    // writeValue(value: any) {
    //   var newValue = value;
    //   super.writeValue(value);
    //   this.selectedItem = this.value;
    //   if (this.selectedItem != null) {
    //     this.selectedItemText = this.resolve(this.displayProperty, this.selectedItem);
    //   }
  
    //   console.log('in dropdown write value', newValue);
    // }
  
    ngOnChanges(changes: any) {
      console.log('drop down in ng changes', changes, this.value, this.identifier);
      if ('selectedItem' in changes) {
        console.log('setting new value2', changes['selectedItem'].currentValue)
        if (changes['selectedItem'].currentValue != this.value) {
          console.log('setting new value', changes['selectedItem'].currentValue)
          this.selectItem(changes['selectedItem'].currentValue);
        }
      }
    }
  
    gotFocus() {
      if (this.readonly) return;
      console.log('got focus');
      if (this.items != null && this.items.length > 0) {
        this.showResults = true;
      }
      this.activeIndex = null;
    }
  
    documentClick(event: any) {
      if (!this._eref.nativeElement.contains(event.target)) {
       this.showResults = false;
      }
    }

    resolve(stringPath: any, baseObject: any): any {
      return stringPath.split('.').reduce((p: any, q: any) => {
      return p ? p[q] : null;
      } , baseObject|| self);
    }
  
    @HostListener('window:keyup', ['$event'])
    keyEvent(event: KeyboardEvent) {
      console.log(event.keyCode);
      if (this.items == null || this.items.length == 0 || !this.showResults) {
        return;
      }
  
      if (event.keyCode != 40 && event.keyCode != 38 && event.keyCode != 13) {
        return;
      }
      event.stopPropagation();
      event.cancelBubble = true;
      event.preventDefault();
      if (event.keyCode === 13) { //enter
        if (this.activeIndex >= 0 && this.activeIndex < this.items.length) {
          this.onItemSelected(event, this.items[this.activeIndex]);
        }
        return;
      }
      if (event.keyCode == 40) { //down      
        if (this.activeIndex == null) {
          this.activeIndex = 0;
        } else {
          if (this.activeIndex == this.items.length - 1) {
            return;
          }
          this.activeIndex = this.activeIndex + 1;
        }
      }
  
      if (event.keyCode === 38) { //up
        if (this.activeIndex == null) {
          this.activeIndex = this.items.length;
        } else {
          if (this.activeIndex == 0) {
            return;
          }
          this.activeIndex--;
        }
      }
      if (this.activeIndex > 5) {
        this.scrollToView(this.activeIndex - 2);
      }
    }
  
    scrollToView(index: number) {
      var elements = document.querySelectorAll('.item');
      var len = elements.length;
      const el = elements[index - 1] as HTMLElement;
      if (el != null)
        el.scrollIntoView({ behavior: "smooth" });
    }
  
    selectedValue: any = null;
  
    onItemSelected(event: any, item: any) {
      event.stopPropagation();
      this.selectItem(item);
    }
  
    selectItem(item: any) {
      //this.selectedItem = item;
      this.value = item;
      if (!item)
        return;
  
      this.selectedItemText = this.resolve(this.displayProperty, item);
      this.showResults = false;
      this.change.emit(this.value);
      console.log('item selected', this.value);
      return;
    }
  }
  
  let identifier = 0;
  