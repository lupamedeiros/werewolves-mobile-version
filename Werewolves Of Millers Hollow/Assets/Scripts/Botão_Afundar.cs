using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class Botão_Afundar : MonoBehaviour, IPointerDownHandler, IPointerUpHandler

{
      public Sprite botaopiezoPressionado;
      private Sprite botaopiezoNaoPressionado;
  
      private Vector3 tamanhoOriginal;
  
      private Image spriteAtualdoBotao;
  
      public float variacaoDoTamanho;
  
      void Start()
      {
          spriteAtualdoBotao = GetComponent<Image>();
          botaopiezoNaoPressionado = spriteAtualdoBotao.sprite;
          tamanhoOriginal = transform.localScale;
      }
      
      public void OnPointerDown(PointerEventData eventData)
      {
          transform.localScale = tamanhoOriginal * variacaoDoTamanho;
          
          spriteAtualdoBotao.sprite = botaopiezoPressionado;
      }
      
      public void OnPointerUp(PointerEventData eventData)
      {
          transform.localScale = tamanhoOriginal;
         
          spriteAtualdoBotao.sprite = botaopiezoNaoPressionado;
      }

}
