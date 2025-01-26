using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventCardVisuals : MonoBehaviour
{
    [SerializeField] private TMP_Text title, desc;
    [SerializeField] private Image image;
    [SerializeField] private RectTransform followPoint;
    private float lerpSpeed = 5;
    // Start is called before the first frame update
    public void Setup(EventCardData eventCardData)
    {
        title.text = eventCardData.displayName;
        desc.text = eventCardData.description;
        image.sprite = eventCardData.preview;
    }

    public Transform GetFollowPoint() { return followPoint; }
    private void Update()
    {
        if (followPoint == null)
            Destroy(gameObject);
        else
            transform.position = Vector3.Lerp(transform.position, followPoint.position, Time.deltaTime * lerpSpeed);
    }
}
