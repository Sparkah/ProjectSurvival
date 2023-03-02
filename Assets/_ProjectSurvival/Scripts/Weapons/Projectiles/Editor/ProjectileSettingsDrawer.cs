using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ProjectileSettings))]
public class ProjectileSettingsDrawer : PropertyDrawer
{
    private const int PropertyDrawerHeight = 50;
    private const int FieldsOffset = 5;
    private OutputtingField[] _outputtingFields = new OutputtingField[]
    {
        new OutputtingField("Damage","_damage",60),
        new OutputtingField("Size","_size",40),
        new OutputtingField("Speed","_speed",50),
        new OutputtingField("Durability","_durability",60),
    };

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        var indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        Rect previousRect = Rect.zero;
        Rect propertyRect;
        for (int i = 0; i < _outputtingFields.Length; i++)
        {
            propertyRect = CalculateRect(
                position, 
                previousRect,
                _outputtingFields[i].FieldWidth,
                FieldsOffset);

            ShowProperty(
                propertyRect,
                _outputtingFields[i].Title,
                property.FindPropertyRelative(_outputtingFields[i].PropertyName));

            previousRect = propertyRect;
        }

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return PropertyDrawerHeight;
    }

    private Rect CalculateRect(Rect position, Rect previous, int width, int offset)
    {
        float x = previous != Rect.zero ? previous.x + previous.width + offset : position.x;
        return new Rect(x, position.y, width, position.height);
    }

    private void ShowProperty(Rect rect, string title, SerializedProperty property)
    {
        Rect labelRect = new Rect(rect);
        labelRect.height *= 0.5f; //Half height for label

        EditorGUI.LabelField(labelRect, title);

        Rect fieldRect = new Rect(rect);
        fieldRect.y += fieldRect.height * 0.5f; //Half height for value
        fieldRect.height *= 0.5f;

        if (property.propertyType == SerializedPropertyType.Float)
            property.floatValue = EditorGUI.FloatField(fieldRect, property.floatValue);
        else if (property.propertyType == SerializedPropertyType.Integer)
            property.intValue = EditorGUI.IntField(fieldRect, property.intValue);
    }

    private struct OutputtingField
    {
        private string _title;
        private string _propertyName;
        private int _fieldWidth;

        public string Title => _title;
        public string PropertyName => _propertyName;
        public int FieldWidth => _fieldWidth;

        public OutputtingField(string title, string propertyName, int fieldWidth)
        {
            _title = title;
            _propertyName = propertyName;
            _fieldWidth = fieldWidth;
        }
    }
}
