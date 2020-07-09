IEnumerator GetSpriteFromUrl()
{
  string _url ="";
  Sprite _img;
  WWW www = new WWW(_url);
  yield return www;
  _img = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
}
