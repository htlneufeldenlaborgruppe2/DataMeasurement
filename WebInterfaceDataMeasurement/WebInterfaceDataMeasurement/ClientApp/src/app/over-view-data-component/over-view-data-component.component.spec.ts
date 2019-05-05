import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { OverViewDataComponentComponent } from './over-view-data-component.component';

describe('OverViewDataComponentComponent', () => {
  let component: OverViewDataComponentComponent;
  let fixture: ComponentFixture<OverViewDataComponentComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ OverViewDataComponentComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(OverViewDataComponentComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
