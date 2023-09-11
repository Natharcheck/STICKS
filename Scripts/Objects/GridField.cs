using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridField: MonoBehaviour
{
     [SerializeField] public Vector2 sizeGrid;
     [SerializeField] private Cell prefabCell;
     [Space]
     [SerializeField] public List<Cell> cells;
     [SerializeField] public List<Stick> sticks;
     
     [HideInInspector] public byte STICKS_COUNT;
     [HideInInspector] public byte tempQuantityMovements;
     [HideInInspector] public byte tempQuantityCells;
     
     private void Start()
     {
          Initialization();
     }
     
     private void Initialization()
     {
          DrawCellsGrid(sizeGrid.x); 
          
          Invoke("ListUpdateSides",0.05f);
          Invoke("ListUpdateSticks",0.1f);
          Invoke("ListUpdateSticksIndex",0.15f);
          Invoke("ListUpdateCellsIndex",0.2f);
          Invoke("ListUpdateComponents",0.25f);
     }

     public int SetHighLimited(int heightMax)
     {
          return heightMax = (((int) sizeGrid.x + (int) sizeGrid.y) / 2 + 2) * 2;
     }
     public void DrawCellsGrid(float value)
     {
          sizeGrid.x = (int) value;
          sizeGrid.y = (int) value;

          for (int y = 0; y < sizeGrid.y; y++)
          {
               for (int x = 0; x < sizeGrid.x; x++)
               {
                    cells.Add(Instantiate(
                         prefabCell, 
                         new Vector3(x, 0, y),
                         prefabCell.transform.rotation));
               }
          }

          for (int y = 0; y < cells.Count; y++)
          {
               cells[y].transform.parent = this.transform;
          }
          
          tempQuantityMovements = 1;
     }

     private void ListUpdateSides() 
     {
          for (int i = 0; i < cells.Count; i++)
          {
               if (cells[i].transform.position.z < (sizeGrid.y - 1))
                    cells[i].DeleteStick(0);
               
               if (cells[i].transform.position.x < (sizeGrid.x - 1))
                    cells[i].DeleteStick(2);
          }
          
          Debug.Log("ListUpdateSides: DONE");
     }
     
     private void ListUpdateSticks()
     {
          for (int i = 0; i < cells.Count; i++) 
          {
               for (int j = 0; j < cells[i].sticks.Count; j++)
               {
                    if (cells[i].sticks[j] != null) 
                         sticks.Add(cells[i].sticks[j]); 
               }
          }
          
          STICKS_COUNT = (byte)sticks.Count;
          Debug.Log("ListUpdateSticks: DONE");
     }
     
     private void ListUpdateSticksIndex()
     {
          for (byte i = 0; i < sticks.Count; i++)
               sticks[i].index += i;
          
          Debug.Log("ListUpdateSticksIndex: DONE");
     }
     
     private void ListUpdateCellsIndex()
     {
          for (byte i = 0; i < cells.Count; i++)
               cells[i].index += i;
          

          Debug.Log("ListUpdateCellsIndex: DONE");
     }

     private void ListUpdateComponents() 
     {
          for (int i = 0; i < cells.Count; i++)
          {
               if (cells[i].sticks[2] == null) 
                    cells[i].sticks[2] = cells[i + 1].GetComponent<Cell>().sticks[1]; 
               
               if (cells[i].sticks[0] == null) 
                    cells[i].sticks[0] = cells[i + (int)sizeGrid.x].GetComponent<Cell>().sticks[3]; 
          }
          Debug.Log("ListUpdateComponents: DONE");
     }

     public void SetExaminationPositionStick(Stick _stick, Cell _cell)
     {
          /*______________ Z POSITION FIELD ___________*/
          if (_stick.positionSide == Stick.PositionSide.Down &&
              _cell.position.z > 0f)
          {
               GetCellDown(_cell.cellsSide, _stick, _cell.index);
          }
          else if (_stick.positionSide == Stick.PositionSide.Down &&
                   _cell.position.z <= 0f)
          {
               GetCell(_stick, _cell.index);
          }
          else if (_stick.positionSide == Stick.PositionSide.Up)
          {
               GetCell(_stick, _cell.index);
          }
          
          /*______________ X POSITION FIELD ___________*/
          if (_stick.positionSide == Stick.PositionSide.Left &&
              _cell.position.x > 0f)
          {
               GetCellLeft(_cell.cellsSide, _stick, _cell.index);
          }
          else if (_stick.positionSide == Stick.PositionSide.Left &&
                   _cell.position.x <= 0f)
          {
               GetCell(_stick, _cell.index);
          }
          else if (_stick.positionSide == Stick.PositionSide.Right)
          {
               GetCell(_stick, _cell.index);
          }

          
     }

     private void GetCellDown(List<Cell> _cellsSide, Stick _stick, int index)
     {
          for (int i = 0; i < cells.Count; i++)
          {
               if (i >= (int)sizeGrid.x && 
                   cells[index].sticks[3].index == cells[i - (int)sizeGrid.x].sticks[0].index)
               { 
                    _cellsSide[0] = cells[i - (int)sizeGrid.x];
                    _cellsSide[0].quantitySticks += 1;
               }
          }

          GetCell(_stick, index);

          if (_cellsSide[0].quantitySticks >= 4)
          {
               _cellsSide[0].quantitySticks = 4;
               _cellsSide[0].FillingCellColor(_stick);
               
               tempQuantityCells += 1;
               tempQuantityMovements = 2;
          }
        
     }
     
     private void GetCellLeft(List<Cell> _cellsSide, Stick _stick, int index)
     {
           for (int i = 0; i < cells.Count; i++)
           { 
                if (i >= 1 && 
                    cells[index].sticks[1].index == cells[i - 1].sticks[2].index) 
                { 
                    _cellsSide[1] = cells[i - 1]; 
                    _cellsSide[1].quantitySticks += 1;
                }
           }

           GetCell(_stick, index);

           if (_cellsSide[1].quantitySticks >= 4)
           {
                _cellsSide[1].quantitySticks = 4;
                _cellsSide[1].FillingCellColor(_stick);
                
                tempQuantityCells += 1;
                tempQuantityMovements = 2;
           }

     }
     
     private void GetCell(Stick _stick, int index)
     {
          cells[index].quantitySticks += 1;

          if (cells[index].quantitySticks >= 4)
          {
               cells[index].quantitySticks = 4;
               cells[index].FillingCellColor(_stick);

               tempQuantityCells += 1;
               tempQuantityMovements = 2;
          }
          else if (cells[index].quantitySticks < 4)
               tempQuantityMovements = 1;
     }

     public void GetEventStick(List<EventTrigger> eventTriggers)
     {
          for (int i = 0; i < STICKS_COUNT; i++)
               eventTriggers.Add(sticks[i].eventTrigger);
     }
     
}